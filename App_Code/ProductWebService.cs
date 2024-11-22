using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;


/// <summary>
/// Summary description for ProductWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
 
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ProductWebService : System.Web.Services.WebService
{   
    public ProductWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public static string json_file = AppDomain.CurrentDomain.BaseDirectory + "\\json_products.json";

    [WebMethod(MessageName = "Create_New_Prodcut")]
    public string CreateProduct(int Id, string Name, string Description, float Price)
    {
        Product product = new Product
        {
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price
        };

        string json_product = JsonConvert.SerializeObject(product, Formatting.Indented);

        try
        {
            System.IO.File.WriteAllText(json_file, json_product);

            return "Product '" + product.Name + "' has been created successfuly";
        }
        catch (Exception)
        {

            return "Something went wrong!.. Product has not been created";
        }
    }

    [WebMethod(MessageName = "Update_Prodcut")]
    public string UpdateProduct(int Id, string Name, string Description, float Price)
    {
        Product product;

        using (StreamReader r = new StreamReader(json_file))
        {
            string json = r.ReadToEnd();
            product = JsonConvert.DeserializeObject<Product>(json);    
        }

        if (product.Id == Id)
        {
            product.Name = Name;
            product.Description = Description;
            product.Price = Price;

            string json_product = JsonConvert.SerializeObject(product, Formatting.Indented);
            System.IO.File.WriteAllText(json_file, json_product);

            return "Product '" + product.Name + "' has been updated successfuly";
        }
        else
        {
            return "Prodcut Not Found.. Please check product Id";
        }
    }

    [WebMethod(MessageName = "Get_Prodcut_Details")]
    public string GetProductDetails(int Id)
    {
        using (StreamReader r = new StreamReader(json_file))
        {
            string json = r.ReadToEnd();
            Product product = JsonConvert.DeserializeObject<Product>(json);

            if(product.Id == Id)
            {
                return JsonConvert.SerializeObject(product, Formatting.Indented);
            }
            else
            {
                return "Prodcut Not Found..";
            }
        }
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
}
