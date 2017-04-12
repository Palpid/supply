using HKSupply.DB;
using HKSupply.Exceptions;
using HKSupply.Forms;
using HKSupply.Helpers;
using HKSupply.Helpers.Mocking;
using HKSupply.Models;
using HKSupply.Services.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HKSupply
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //InitDBData();

            CustomerDBTest();

            //************ Test log4net ************//
            //var userEF = new EFUser();
            //try
            //{
            //    var user = userEF.GetUserByLoginPassword(null, "xx"); //ArgumentNullException
            //}
            //catch (ArgumentNullException anex) {}

            //try
            //{
            //    var user = userEF.GetUserByLoginPassword("XX", null); //ArgumentNullException
            //}
            //catch (ArgumentNullException anex){}


            //try
            //{
            //    var user = userEF.GetUserByLoginPassword("XX", "XX");
            //}
            //catch (NonexistentUserException neuex){}



            //using (var db = new HKSupplyContext())
            //{
            //    //Load all user and his related role
            //    var users = db.Users.Include("UserRol").ToList();
            //    var user2 = db.Users.Include(r => r.UserRol).ToList();
            //}

            //Encriptar el password
        
            //string pass = "miPassword";
            //string hash = PasswordHelper.GetHash(pass);
            //Console.WriteLine(hash);
            //bool isValid = PasswordHelper.ValidatePass("miPassword2", hash);
            //Console.WriteLine(isValid ? "válida" : "no válida");
            //isValid = PasswordHelper.ValidatePass("miPassword", hash);
            //Console.WriteLine(isValid ? "válida" : "no válida");
            
        }

        private void InitDBData()
        {
            try
            {
                //Entity Framework

                //***** Role *****//
                using (var db = new HKSupplyContext())
                {

                    var roleAdmin = new Role
                    {
                        RoleId = "ADMIN",
                        Description = "Application Administrator",
                        Enabled = true,
                        Remarks = null
                    };

                    var roleOperator = new Role
                    {
                        RoleId = "OPERATOR",
                        Description = "Operator",
                        Enabled = true,
                        Remarks = null
                    };

                    db.Roles.Add(roleAdmin);
                    db.Roles.Add(roleOperator);
                    db.SaveChanges();

                    //var query = from b in db.Roles
                    //            orderby b.RoleId
                    //            select b;
                    //foreach (var item in query)
                    //{
                    //    MessageBox.Show(item.Description);
                    //}
                                        
                    //*****users *****//
                    
                    var userAdmin = new User
                    {
                        UserLogin = "admin",
                        Name = "Administrator",
                        Password = PasswordHelper.GetHash("adminpwd"),
                        UserRole = roleAdmin,
                        Enabled = true,
                        LastLogin = null,
                        LastLogout = null,
                        Remarks = null
                    };

                    var userMario = new User
                    {
                        UserLogin = "m.ruz",
                        Name = "Mario Ruz Martínez",
                        Password = PasswordHelper.GetHash("mariopwd"),
                        UserRole = roleAdmin,
                        Enabled = true,
                        LastLogin = null,
                        LastLogout = null,
                        Remarks = null
                    };

                    var userOp1 = new User
                    {
                        UserLogin = "operator1",
                        Name = "Operator 1",
                        Password = PasswordHelper.GetHash("op1pwd"),
                        UserRole = roleOperator,
                        Enabled = true,
                        LastLogin = null,
                        LastLogout = null,
                        Remarks = null
                    };

                    db.Users.Add(userAdmin);
                    db.Users.Add(userMario);
                    db.Users.Add(userOp1);
                    
                    db.SaveChanges();

                    //var queryUser = from u in db.Users
                    //                orderby u.UserLogin
                    //                select u;

                    //foreach (var item in queryUser)
                    //{
                    //    MessageBox.Show(item.UserLogin);
                    //}
                    
                    //***** Functionalities *****//
                    
                    var funcUM = new Functionality
                    {
                        FunctionalityName = "UserManagement",
                        Category = "Masters",
                        FormName = "frmUserManagement"
                    };

                    var funcRM = new Functionality
                    {
                        FunctionalityName = "RoleManagement",
                        Category = "Masters",
                        FormName = "frmRoleManagement"
                    };

                    var funcFM = new Functionality
                    {
                        FunctionalityName = "FunctionalityManagement",
                        Category = "Masters",
                        FormName = "frmFunctionalityManagement"
                    };

                    var funcMMA = new Functionality
                    {
                        FunctionalityName = "MaterialsManagement",
                        Category = "Masters",
                        FormName = "frmMaterialsManagement"

                    };


                    db.Functionalities.Add(funcUM);
                    db.Functionalities.Add(funcRM);
                    db.Functionalities.Add(funcFM);
                    db.Functionalities.Add(funcMMA);
                    db.SaveChanges();

                    //var queryFunc = from f in db.Functionalities
                    //                orderby f.FunctionalityName
                    //                select f;

                    //foreach (var item in queryFunc)
                    //{
                    //    MessageBox.Show(item.FunctionalityName);
                    //}

                    //***** Functionalities Role *****//
                    var adminRole = db.Roles.FirstOrDefault(r => r.RoleId.Equals("ADMIN"));
                    var opRole = db.Roles.FirstOrDefault(r => r.RoleId.Equals("OPERATOR"));

                    var funcUserManagement = db.Functionalities.FirstOrDefault(f => f.FunctionalityName.Equals("UserManagement"));
                    var funcRoleManagement = db.Functionalities.FirstOrDefault(f => f.FunctionalityName.Equals("RoleManagement"));
                    var funcFunctionalityManagement = db.Functionalities.FirstOrDefault(f => f.FunctionalityName.Equals("FunctionalityManagement"));
                    var funcMaterialsManagement = db.Functionalities.FirstOrDefault(f => f.FunctionalityName.Equals("MaterialsManagement"));

                    var fr1 = new FunctionalityRole 
                    { 
                        RoleId = adminRole.RoleId,
                        FunctionalityId = funcUserManagement.FunctionalityId,
                        Read = true,
                        New = true,
                        Modify = true,
                    };

                    var fr2 = new FunctionalityRole
                    {
                        RoleId = adminRole.RoleId,
                        FunctionalityId = funcRoleManagement.FunctionalityId,
                        Read = true,
                        New = true,
                        Modify = true,
                    };

                    var fr3 = new FunctionalityRole
                    {
                        RoleId = adminRole.RoleId,
                        FunctionalityId = funcFunctionalityManagement.FunctionalityId,
                        Read = true,
                        New = true,
                        Modify = true,
                    };

                    var fr4 = new FunctionalityRole
                    {
                        RoleId = adminRole.RoleId,
                        FunctionalityId = funcMaterialsManagement.FunctionalityId,
                        Read = true,
                        New = true,
                        Modify = true,
                    };

                    var fr5 = new FunctionalityRole
                    {
                        RoleId = opRole.RoleId,
                        FunctionalityId = funcMaterialsManagement.FunctionalityId,
                        Read = true,
                        New = false,
                        Modify = false,
                    };

                    db.FunctionalitiesRole.Add(fr1);
                    db.FunctionalitiesRole.Add(fr2);
                    db.FunctionalitiesRole.Add(fr3);
                    db.FunctionalitiesRole.Add(fr4);
                    db.FunctionalitiesRole.Add(fr5);
                    db.SaveChanges();

                }

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        private void CustomerDBTest()
        {
            try
            {
                var customerService = new EFCustomer();

                //Customer newCust = new Customer();
                //newCust.idCustomer = "Customer01";
                //newCust.CustName = "Customer 01";
                //newCust.Active = true;
                //newCust.VATNum = "0101010101";
                //newCust.ShipingAddress = "Customer 01 Shiping Address";
                //newCust.BillingAddress = "Customer 01 Billing Address";
                //newCust.ContactName = "Customer 01 Contact Name";
                //newCust.ContactPhone = "111223344";
                //newCust.idIncoterm = 1;
                //newCust.idPaymentTerms = 2;
                //newCust.Currency = "euro";

                //var res = customerService.NewCustomer(newCust);

                //Customer customer;
                //customer = customerService.GetCustomerById("Customer01");
                //customer.ContactPhone = "5555667788";
                //customer.ShippingAddress += " X";
                //customer.BillingAddress += " X";

                //customerService.UpdateCustomer(customer);

                //customer = customerService.GetCustomerById("Customer01");
                //customer.ContactPhone = "999112233";
                //customer.ShippingAddress += " X";
                //customer.BillingAddress += " X";

                //customerService.UpdateCustomer(customer);

                Customer newCust2 = new Customer();
                newCust2.IdCustomer = "Customer02";
                newCust2.CustomerName = "Customer 02";
                newCust2.Active = true;
                newCust2.VATNum = "0202020202";
                //newCust2.ShippingAddress = "Customer 02 Shiping Address";
                //newCust2.BillingAddress = "Customer 02 Billing Address";
                //newCust2.ContactName = "Customer 02 Contact Name";
                //newCust2.ContactPhone = "444332211";
                //newCust2.IdIncoterm = 3;
                //newCust2.IdPaymentTerms = 4;
                //newCust2.Currency = "US dolar";

                var res = customerService.NewCustomer(newCust2);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main frm = new Main();
            frm.Show();
        }

        private void stackView1_EditButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Edit Button");
        }

        private void stackView1_NewButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("New Button");
        }

        private void stackView1_SaveButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Save Button");
        }
        private void stackView1_CancelButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Cancel Button");
        }


        
    }


}
