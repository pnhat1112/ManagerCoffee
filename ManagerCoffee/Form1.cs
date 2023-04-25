using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ManagerCoffee
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        /*string connetionString = @"Data Source=SONNE\SUN;Initial Catalog=COFFEE_MN;User ID=sa;Password=123456";*/
        string connetionString = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cbBoxConnect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbBoxConnect.SelectedIndex)
            {
                case 0:
                    conn = null;
                    connetionString = @"Data Source=SONNE\SUN;Initial Catalog=COFFEE_MN;User ID=sa;Password=123456";
                    MessageBox.Show("Connection to Main Server ");
                    ChangeServer();
                    break;
                case 1:
                    conn = null;
                    connetionString = @"Data Source=SONNE\SUNSERVER1;Initial Catalog=COFFEE_MN;User ID=sa;Password=123456";
                    MessageBox.Show("Connection to Da Nang Server ");
                    ChangeServer();
                    break;
                case 2:
                    conn = null;
                    connetionString = @"Data Source=SONNE\SUNSERVER2;Initial Catalog=COFFEE_MN;User ID=sa;Password=123456";
                    MessageBox.Show("Connection to Ho Chi Minh Server 2 ");
                    ChangeServer();
                    break;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            cbBoxConnect.SelectedIndex = 0;
            try
            {
                ViewListShipping();
                ViewListProduct();
                ViewListStaff();
                ViewListStorage();
                ViewListBill();
                ChooseStaffID();
                ChooseStorageID();
                ChooseShippingID();
                ChooseProductID();
                /*conn.Open();
                SqlCommand sc = new SqlCommand("Select staffid from staffs", conn);
                SqlDataReader reader;
                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("staffid", typeof(int));
                dt.Load(reader);
                cbStaffID.ValueMember = "staffid";
                cbStaffID.DataSource = dt;
                conn.Close();*/

                /*String queryGetShipping = "SELECT * FROM Shipping INNER JOIN Shipping_manage ON Shipping.ShippingID=Shipping_manage.ShippingID";
                SqlCommand cmd = new SqlCommand(queryGetShipping, conn);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    string output = "OUT: = " + read.GetValue(0) + "-" + read.GetValue(1) + "-" + read.GetValue(2) + read.GetValue(3) + "-" + read.GetValue(4) + "-" + read.GetValue(5) + read.GetValue(8) + "-" + read.GetValue(9);
                    MessageBox.Show(output);
                }*/
                /*string KindOfShipping = "C01";
                String query = $"SELECT COUNT(*) FROM STUDENT WHERE KindOfShipping = '{KindOfShipping}'";
                SqlCommand command = new SqlCommand(query, conn);
                int result = Convert.ToInt32(command.ExecuteScalar());
                MessageBox.Show(result.ToString());*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Connection " + ex.Message);
            }
        }
        
        private void ChooseStaffID()
        {
            try {
                conn.Open();
                SqlCommand sc = new SqlCommand("Select staffid from staffs", conn);
                SqlDataReader reader;
                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("staffid", typeof(int));
                dt.Load(reader);
                cbStaffID.ValueMember = "staffid";
                cbStaffID.DataSource = dt;

                cboxStaffIDTabBill.ValueMember = "staffid";
                cboxStaffIDTabBill.DataSource = dt;
                conn.Close();
            } catch (Exception ex)
            {
                MessageBox.Show("Failed to Connection" + ex.Message);
            }
        }

        private void ChooseProductID()
        {
            try
            {
                conn.Open();
                SqlCommand sc = new SqlCommand("Select productid from products", conn);
                SqlDataReader reader;
                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("productid", typeof(int));
                dt.Load(reader);
                txtProduct.ValueMember = "productid";
                txtProduct.DataSource = dt;

                txtProductIDTabStorage.ValueMember = "productid";
                txtProductIDTabStorage.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Connection" + ex.Message);
            }
        }
        private void ChangeServer()
        {
            ViewListShipping();
            ViewListProduct();
            ViewListStaff();
            ViewListStorage();
            ViewListBill();
            ChooseStaffID();
            ChooseStorageID();
            ChooseShippingID();
        }

        private void tabShipping_Click(object sender, EventArgs e)
        {

        }

        private void listViewShipping_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewShipping.SelectedItems.Count > 0)
            {
                txtShippingID.Text = listViewShipping.SelectedItems[0].SubItems[0].Text;
                txtDate.Text = listViewShipping.SelectedItems[0].SubItems[1].Text;
                txtKind.Text = listViewShipping.SelectedItems[0].SubItems[2].Text;
                cbStaffID.Text = listViewShipping.SelectedItems[0].SubItems[3].Text;
                txtSource.Text = listViewShipping.SelectedItems[0].SubItems[4].Text;
                txtDestination.Text = listViewShipping.SelectedItems[0].SubItems[5].Text;
                txtProduct.Text = listViewShipping.SelectedItems[0].SubItems[6].Text;
                txtQuantity.Text = listViewShipping.SelectedItems[0].SubItems[7].Text;
            }
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            ViewListShipping();
            ChooseStaffID();
            ChooseProductID();
        }

        private void ViewListShipping()
        {
            if (conn == null) conn = new SqlConnection(connetionString);
            if (conn.State == ConnectionState.Closed) conn.Open();

            listViewShipping.Items.Clear();
            if (conn.State == ConnectionState.Closed) conn.Open();
            String queryGetShipping = "SELECT * FROM shippings INNER JOIN shipping_manages ON shippings.shippingid=shipping_manages.shippingid";
            SqlCommand commandGetShipping = new SqlCommand(queryGetShipping, conn);
            SqlDataReader readShipping = commandGetShipping.ExecuteReader();

            while (readShipping.Read())
            {
                string ShippingID = readShipping.GetInt32(0).ToString();
                DateTime dateFromDatabase = readShipping.GetDateTime(1);
                string Date = dateFromDatabase.ToString("yyyy-MM-dd");
                string King_of_shipping = readShipping.GetString(2);
                string StaffID = readShipping.GetInt32(3).ToString();
                string SourceID = readShipping.GetInt32(4).ToString();
                string DestinationID = readShipping.GetInt32(5).ToString();
                string ProductID = readShipping.GetInt32(8).ToString();
                string Quantity = readShipping.GetInt32(9).ToString();
                /*string QuantityString = Quantity.ToString();*/

                ListViewItem item = listViewShipping.Items.Add(ShippingID);
                item.SubItems.Add(Date);
                item.SubItems.Add(King_of_shipping);
                item.SubItems.Add(StaffID);
                item.SubItems.Add(SourceID);
                item.SubItems.Add(DestinationID);
                item.SubItems.Add(ProductID);
                item.SubItems.Add(Quantity);
            }
            conn.Close();
        }
        int result = -1;
        int result1 = -1;
        private void btnInsert_Click(object sender, EventArgs e)
        {


            if (String.IsNullOrEmpty(txtShippingID.Text.Trim()) ||
                String.IsNullOrEmpty(txtDate.Text.Trim()) ||
                String.IsNullOrEmpty(txtKind.Text.Trim()) ||
                String.IsNullOrEmpty(cbStaffID.Text.Trim()) ||
                String.IsNullOrEmpty(txtSource.Text.Trim()) ||
                String.IsNullOrEmpty(txtDestination.Text.Trim()) ||
                String.IsNullOrEmpty(txtProduct.Text.Trim()) ||
                String.IsNullOrEmpty(txtQuantity.Text.Trim()))
            {
                MessageBox.Show("NULL");
            }
            else
            {
                if (conn == null) conn = new SqlConnection(connetionString);
                if (conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO shippings(date,kind_of_shipping,staffid,sourceid,destinationid)"
                    + "VALUES(@Date,@Kind_of_shipping,@StaffID,@SourceID,@DestinationID); SELECT SCOPE_IDENTITY()";

                /*SqlParameter parameter_ShippingID = new SqlParameter("@ShippingID", txtShippingID.Text);*/
                /* int parameter_ShippingID = int.Parse(txtShippingID.Text);
                 cmd.Parameters.Add(parameter_ShippingID);*/
                SqlParameter parameter_Date = new SqlParameter("@Date", txtDate.Text);
                cmd.Parameters.Add(parameter_Date);
                SqlParameter parameter_KindOfShipping = new SqlParameter("@Kind_of_shipping", txtKind.Text);
                cmd.Parameters.Add(parameter_KindOfShipping);
                SqlParameter parameter_StaffID = new SqlParameter("@StaffID", cbStaffID.Text);
                cmd.Parameters.Add(parameter_StaffID);
                SqlParameter parameter_SourceID = new SqlParameter("@SourceID", txtSource.Text);
                cmd.Parameters.Add(parameter_SourceID);
                SqlParameter parameter_DestinationID = new SqlParameter("@DestinationID", txtDestination.Text);
                cmd.Parameters.Add(parameter_DestinationID);


                

                try
                {
                    int shippingid = Convert.ToInt32(cmd.ExecuteScalar());
                    /*Console.WriteLine(result);
                    MessageBox.Show(shippingid.ToString());*/
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = conn;
                    cmd1.CommandText = "INSERT INTO [shipping_manages](shippingid, productid,quantity)"
                        + "VALUES(@ShippingID,@ProductID,@Quantity)";
                    SqlParameter parameter_ShippingID1 = new SqlParameter("@ShippingID", shippingid);
                    cmd1.Parameters.Add(parameter_ShippingID1);
                    SqlParameter parameter_ProductID = new SqlParameter("@ProductID", txtProduct.Text);
                    cmd1.Parameters.Add(parameter_ProductID);
                    /*int Quantity = int.Parse(txtQuantity.Text);*/
                    SqlParameter parameter_Quantity = new SqlParameter("@Quantity", txtQuantity.Text);
                    cmd1.Parameters.Add(parameter_Quantity);

                    result1 = cmd1.ExecuteNonQuery();
                    MessageBox.Show("Insert Success!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Insert Failed!!");
                }
                if (result1 > 0)
                {
                    ViewListShipping();
                }

            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtShippingID.Text.Trim()) ||
                String.IsNullOrEmpty(txtDate.Text.Trim()) ||
                String.IsNullOrEmpty(txtKind.Text.Trim()) ||
                String.IsNullOrEmpty(cbStaffID.Text.Trim()) ||
                String.IsNullOrEmpty(txtSource.Text.Trim()) ||
                String.IsNullOrEmpty(txtDestination.Text.Trim()) ||
                String.IsNullOrEmpty(txtProduct.Text.Trim()) ||
                String.IsNullOrEmpty(txtQuantity.Text.Trim()))
            {
                MessageBox.Show("NULL");
            }
            else
            {
                if (conn == null) conn = new SqlConnection(connetionString);
                if (conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE shippings SET date='{txtDate.Text}', kind_of_shipping='{txtKind.Text}'" +
                    $", staffid='{cbStaffID.Text}', sourceid='{txtSource.Text}', destinationid='{txtDestination.Text}'" +
                    $" WHERE shippingid='{txtShippingID.Text}'", conn);

                SqlCommand cmd1 = new SqlCommand($"UPDATE shipping_manages SET productid='{txtProduct.Text}', quantity='{txtQuantity.Text}'" +
                    $" WHERE shippingid='{txtShippingID.Text}'", conn);

                try
                {
                    result = cmd.ExecuteNonQuery();
                    result1 = cmd1.ExecuteNonQuery();
                    MessageBox.Show("Update Success!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Update Failed!!");
                }
                if (result > 0)
                {
                    ViewListShipping();
                }
            }

            


        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (conn == null) conn = new SqlConnection(connetionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM [shipping_manages] WHERE shippingid='{txtShippingID.Text}'", conn);
            SqlCommand cmd1 = new SqlCommand($"DELETE FROM [shippings] WHERE shippingid='{txtShippingID.Text}'", conn);

            try
            {
                result1 = cmd.ExecuteNonQuery();
                result = cmd1.ExecuteNonQuery();
                MessageBox.Show("Delete Success!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Delete Failed!!");
            }
            if (result > 0)
            {
                ViewListShipping();
                txtShippingID.Text = "";
                txtDate.Text = "";
                txtKind.Text = "";
                txtSource.Text = "";
                txtDestination.Text = "";
                txtProduct.Text = "";
                txtQuantity.Text = "";
            }

        }

        private void tabProduct_Click(object sender, EventArgs e)
        {
            ViewListProduct();
        }

        private void ViewListProduct()
        {
            if (conn == null) conn = new SqlConnection(connetionString);
            if (conn.State == ConnectionState.Closed) conn.Open();

            listViewProduct.Items.Clear();
            if (conn.State == ConnectionState.Closed) conn.Open();
            String queryGetProduct = "SELECT * FROM products";
            SqlCommand commandGetProduct = new SqlCommand(queryGetProduct, conn);
            SqlDataReader readProduct = commandGetProduct.ExecuteReader();

            while (readProduct.Read())
            {
                string ProductID = readProduct.GetInt32(0).ToString();
                string Name = readProduct.GetString(1);
                string Des = readProduct.GetString(2);
                decimal Price = readProduct.GetDecimal(3);
                string PricetoString = Price.ToString();
                /*string QuantityString = Quantity.ToString();*/

                ListViewItem item = listViewProduct.Items.Add(ProductID);
                item.SubItems.Add(Name);
                item.SubItems.Add(Des);
                item.SubItems.Add(PricetoString);
            }
            conn.Close();
        }

        private void listViewProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewProduct.SelectedItems.Count > 0)
            {
                txtProductIDTabProduct.Text = listViewProduct.SelectedItems[0].SubItems[0].Text;
                txtNameProductTabProduct.Text = listViewProduct.SelectedItems[0].SubItems[1].Text;
                txtDesTabProduct.Text = listViewProduct.SelectedItems[0].SubItems[2].Text;
                txtPriceTabProduct.Text = listViewProduct.SelectedItems[0].SubItems[3].Text;
            }
        }

        private void btnFreshTabProduct_Click(object sender, EventArgs e)
        {
            ViewListProduct();
        }

        private void btnInsertTabProduct_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNameProductTabProduct.Text.Trim()) ||
                String.IsNullOrEmpty(txtDesTabProduct.Text.Trim()) ||
                String.IsNullOrEmpty(txtPriceTabProduct.Text.Trim()))
            {
                MessageBox.Show("NULL");
            }
            else
            {
                if (conn == null) conn = new SqlConnection(connetionString);
                if (conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO products(name,description,price)"
                    + "VALUES(@Name,@Description,@Price)";
                SqlParameter parameter_Name = new SqlParameter("@Name", txtNameProductTabProduct.Text);
                cmd.Parameters.Add(parameter_Name);
                SqlParameter parameter_Description = new SqlParameter("@Description", txtDesTabProduct.Text);
                cmd.Parameters.Add(parameter_Description);
                SqlParameter parameter_Price = new SqlParameter("@Price", txtPriceTabProduct.Text);
                cmd.Parameters.Add(parameter_Price);
                /*int ProductID = int.Parse(txtProductIDTabProduct.Text);
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                string Name = txtNameProductTabProduct.Text;
                cmd.Parameters.AddWithValue("@Name", Name);
                string Description = txtDesTabProduct.Text;
                cmd.Parameters.AddWithValue("@Description", Description);
                decimal Price = decimal.Parse(txtPriceTabProduct.Text);
                cmd.Parameters.AddWithValue("@Price", Price);*/

                try
                {
                    result = cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert Success!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Insert Failed!!");
                }
                if (result > 0)
                {
                    ViewListProduct();
                }
            }
            
        }

        private void btnUpdateTabProduct_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNameProductTabProduct.Text.Trim()) ||
                String.IsNullOrEmpty(txtDesTabProduct.Text.Trim()) ||
                String.IsNullOrEmpty(txtPriceTabProduct.Text.Trim()))
            {
                MessageBox.Show("NULL");
            }
            else
            {
                if (conn == null) conn = new SqlConnection(connetionString);
                if (conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE products SET name='{txtNameProductTabProduct.Text}', description='{txtDesTabProduct.Text}'" +
                    $", price='{txtPriceTabProduct.Text}'" +
                    $" WHERE productid='{txtProductIDTabProduct.Text}'", conn);

                try
                {
                    result = cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Success!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Update Failed!!");
                }
                if (result > 0)
                {
                    ViewListProduct();
                }
            }
        }

        private void btnDelTabProduct_Click(object sender, EventArgs e)
        {
            if (conn == null) conn = new SqlConnection(connetionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM [products] WHERE productid='{txtProductIDTabProduct.Text}'", conn);

            try
            {
                result = cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Success!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Delete Failed!!");
            }
            if (result > 0)
            {
                ViewListProduct();
                txtProductIDTabProduct.Text = "";
                txtNameProductTabProduct.Text = "";
                txtDesTabProduct.Text = "";
                txtPriceTabProduct.Text = "";
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void listViewStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewStorage.SelectedItems.Count > 0)
            {
                cbStorageIDTabStorage.Text = listViewStorage.SelectedItems[0].SubItems[0].Text;
                txtProductIDTabStorage.Text = listViewStorage.SelectedItems[0].SubItems[1].Text;
                txtNameTabStorage.Text = listViewStorage.SelectedItems[0].SubItems[2].Text;
                txtQuantityTabStorage.Text = listViewStorage.SelectedItems[0].SubItems[3].Text;
            }
        }

        private void ChooseStorageID()
        {
            try
            {
                conn.Open();
                SqlCommand sc = new SqlCommand("Select storageid from storages", conn);
                SqlDataReader reader;
                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("storageid", typeof(int));
                dt.Load(reader);
                cbStorageIDTabStorage.ValueMember = "storageid";
                cbStorageIDTabStorage.DataSource = dt;

                cbStorageIDTabStaff.ValueMember = "storageid";
                cbStorageIDTabStaff.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Connection" + ex.Message);
            }
        }

        private void btnRefeshTabStorage_Click(object sender, EventArgs e)
        {
            ViewListStorage();
            ChooseStorageID();
            ChooseProductID();
        }
        private void ViewListStorage()
        {

            if (conn == null) conn = new SqlConnection(connetionString);
            if (conn.State == ConnectionState.Closed) conn.Open();

            listViewStorage.Items.Clear();
            if (conn.State == ConnectionState.Closed) conn.Open();
            String queryGetStorage = "SELECT storage_manages.storageid, storage_manages.productid, products.name, storage_manages.quantity " +
                "FROM storage_manages LEFT JOIN products ON storage_manages.productid = products.productid";
            SqlCommand commandGetStorage = new SqlCommand(queryGetStorage, conn);
            SqlDataReader readStorage = commandGetStorage.ExecuteReader();

            while (readStorage.Read())
            {
                string StorageID = readStorage.GetInt32(0).ToString();
                string ProductID = readStorage.GetInt32(1).ToString();
                string Name = readStorage.GetString(2);
                string Quantity = readStorage.GetInt32(3).ToString();
                /*string QuantityString = Quantity.ToString();*/

                ListViewItem item = listViewStorage.Items.Add(StorageID);
                item.SubItems.Add(ProductID);
                item.SubItems.Add(Name);
                item.SubItems.Add(Quantity);
            }
            conn.Close();
        }

        private void btnInsertTabStorage_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtProductIDTabStorage.Text.Trim()) ||
                String.IsNullOrEmpty(cbStorageIDTabStorage.Text.Trim()) ||
                String.IsNullOrEmpty(txtQuantityTabStorage.Text.Trim()))
            {
                MessageBox.Show("NULL");
            }
            else
            {
                if (conn == null) conn = new SqlConnection(connetionString);
                if (conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO storage_manages(storageid,productid,quantity)"
                    + "VALUES(@StorageID,@ProductID,@Quantity)";
                SqlParameter parameter_StorageID = new SqlParameter("@StorageID", cbStorageIDTabStorage.Text);
                cmd.Parameters.Add(parameter_StorageID);
                SqlParameter parameter_ProductID = new SqlParameter("@ProductID", txtProductIDTabStorage.Text);
                cmd.Parameters.Add(parameter_ProductID);
                SqlParameter parameter_Quantity = new SqlParameter("@Quantity", txtQuantityTabStorage.Text);
                cmd.Parameters.Add(parameter_Quantity);

                try
                {
                    result = cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert Success!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Insert Failed!!");
                }
                if (result > 0)
                {
                    ViewListStorage();
                }
            }
        }

        private void btnUpdateTabStorage_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtProductIDTabStorage.Text.Trim()) ||
                String.IsNullOrEmpty(cbStorageIDTabStorage.Text.Trim()) ||
                String.IsNullOrEmpty(txtQuantityTabStorage.Text.Trim()))
            {
                MessageBox.Show("NULL");
            }
            else
            {
                if (conn == null) conn = new SqlConnection(connetionString);
                if (conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE storage_manages SET productid='{txtProductIDTabStorage.Text}', quantity='{txtQuantityTabStorage.Text}'" +
                    $" WHERE storageid='{cbStorageIDTabStorage.Text}'", conn);

                try
                {
                    result = cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Success!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Update Failed!!");
                }
                if (result > 0)
                {
                    ViewListStorage();
                }
            }
        }

        private void btnDelTabStorage_Click(object sender, EventArgs e)
        {
            if (conn == null) conn = new SqlConnection(connetionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM [storage_manages] WHERE storageid='{cbStorageIDTabStorage.Text}' AND productid='{txtProductIDTabStorage.Text}' ", conn);

            try
            {
                result = cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Success!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Delete Failed!!");
            }
            if (result > 0)
            {
                ViewListStorage();
                cbStorageIDTabStorage.Text = "";
                txtProductIDTabProduct.Text = "";
                txtNameProductTabProduct.Text = "";
                txtQuantityTabStorage.Text = "";
            }
        }

        private void btnRefeshTabStaff_Click(object sender, EventArgs e)
        {
            ViewListStaff();
            ChooseStorageID();
        }

        private void ViewListStaff()
        {
            if (conn == null) conn = new SqlConnection(connetionString);
            if (conn.State == ConnectionState.Closed) conn.Open();

            listViewStaff.Items.Clear();
            if (conn.State == ConnectionState.Closed) conn.Open();
            String queryGetStaff = "SELECT * FROM staffs";
            SqlCommand commandGetStorage = new SqlCommand(queryGetStaff, conn);
            SqlDataReader readStaff = commandGetStorage.ExecuteReader();

            while (readStaff.Read())
            {
                string StaffID = readStaff.GetInt32(0).ToString();
                string Name = readStaff.GetString(1);
                string position = readStaff.GetString(2);
                string StorageID = readStaff.GetInt32(3).ToString();
                /*string QuantityString = Quantity.ToString();*/

                ListViewItem item = listViewStaff.Items.Add(StaffID);
                item.SubItems.Add(Name);
                item.SubItems.Add(position);
                item.SubItems.Add(StorageID);
            }
            conn.Close();
        }

        private void listViewStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewStaff.SelectedItems.Count > 0)
            {
                txtStaffIDTabStaff.Text = listViewStaff.SelectedItems[0].SubItems[0].Text;
                txtNameTabStaff.Text = listViewStaff.SelectedItems[0].SubItems[1].Text;
                txtPostionTabStaff.Text = listViewStaff.SelectedItems[0].SubItems[2].Text;
                cbStorageIDTabStaff.Text = listViewStaff.SelectedItems[0].SubItems[3].Text;
            }
        }

        private void btnInsertTabStaff_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNameTabStaff.Text.Trim()) ||
                String.IsNullOrEmpty(txtPostionTabStaff.Text.Trim()) ||
                String.IsNullOrEmpty(cbStorageIDTabStaff.Text.Trim()))
            {
                MessageBox.Show("NULL");
            }
            else
            {
                if (conn == null) conn = new SqlConnection(connetionString);
                if (conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO staffs(name,position,storageid)"
                    + "VALUES(@Name,@position,@Storageid)";
                SqlParameter parameter_ProductID = new SqlParameter("@Name", txtNameTabStaff.Text);
                cmd.Parameters.Add(parameter_ProductID);
                SqlParameter parameter_StorageID = new SqlParameter("@position", txtPostionTabStaff.Text);
                cmd.Parameters.Add(parameter_StorageID);
                SqlParameter parameter_Quantity = new SqlParameter("@Storageid", cbStorageIDTabStaff.Text);
                cmd.Parameters.Add(parameter_Quantity);

                try
                {
                    result = cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert Success!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Insert Failed!!");
                }
                if (result > 0)
                {
                    ViewListStaff();
                }
            }
        }

        private void btnUpdateTabStaff_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNameTabStaff.Text.Trim()) ||
                String.IsNullOrEmpty(txtPostionTabStaff.Text.Trim()) ||
                String.IsNullOrEmpty(cbStorageIDTabStaff.Text.Trim()))
            {
                MessageBox.Show("NULL");
            }
            else
            {
                if (conn == null) conn = new SqlConnection(connetionString);
                if (conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE staffs SET name='{txtNameTabStaff.Text}', position='{txtPostionTabStaff.Text}', storageid='{cbStorageIDTabStaff.Text}'" +
                    $" WHERE staffid='{txtStaffIDTabStaff.Text}'", conn);

                try
                {
                    result = cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Success!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Update Failed!!");
                }
                if (result > 0)
                {
                    ViewListStaff();
                }
            }
        }

        private void btnDelTabStaff_Click(object sender, EventArgs e)
        {
            if (conn == null) conn = new SqlConnection(connetionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM [staffs] WHERE staffid='{txtStaffIDTabStaff.Text}'", conn);

            try
            {
                result = cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Success!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Delete Failed!!");
            }
            if (result > 0)
            {
                ViewListStaff();
                txtStaffIDTabStaff.Text = "";
                txtNameTabStaff.Text = "";
                txtPostionTabStaff.Text = "";
                cbStorageIDTabStaff.Text = "";
            }
        }

        private void btnRefeshTabBill_Click(object sender, EventArgs e)
        {
            ViewListBill();
            ChooseStaffID();
            ChooseShippingID();
        }

        private void ViewListBill()
        {
            if (conn == null) conn = new SqlConnection(connetionString);
            if (conn.State == ConnectionState.Closed) conn.Open();

            listViewBill.Items.Clear();
            if (conn.State == ConnectionState.Closed) conn.Open();
            String queryGetStaff = "SELECT * FROM bills";
            SqlCommand commandGetStorage = new SqlCommand(queryGetStaff, conn);
            SqlDataReader readStaff = commandGetStorage.ExecuteReader();

            while (readStaff.Read())
            {
                string BillID = readStaff.GetInt32(0).ToString(); 
                DateTime dateFromDatabase = readStaff.GetDateTime(1);
                string Date = dateFromDatabase.ToString("yyyy-MM-dd");
                string TotalPrice = readStaff.GetDecimal(2).ToString();
                string ShippingID = readStaff.GetInt32(3).ToString();
                string StaffID = readStaff.GetInt32(4).ToString();
                /*string QuantityString = Quantity.ToString();*/

                ListViewItem item = listViewBill.Items.Add(BillID);
                item.SubItems.Add(Date);
                item.SubItems.Add(TotalPrice);
                item.SubItems.Add(ShippingID);
                item.SubItems.Add(StaffID);
            }
            conn.Close();
        }

        private void ChooseShippingID()
        {
            try
            {
                conn.Open();
                SqlCommand sc = new SqlCommand("Select shippingid from shippings", conn);
                SqlDataReader reader;
                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("shippingid", typeof(int));
                dt.Load(reader);
                cboxShippingIDTabBill.ValueMember = "shippingid";
                cboxShippingIDTabBill.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Connection" + ex.Message);
            }
        }

        private void listViewBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewBill.SelectedItems.Count > 0)
            {
                txtBillIDTabBill.Text = listViewBill.SelectedItems[0].SubItems[0].Text;
                txtDateTabBill.Text = listViewBill.SelectedItems[0].SubItems[1].Text;
                txtTotalPriceTabBill.Text = listViewBill.SelectedItems[0].SubItems[2].Text;
                cboxShippingIDTabBill.Text = listViewBill.SelectedItems[0].SubItems[3].Text;
                cboxStaffIDTabBill.Text = listViewBill.SelectedItems[0].SubItems[4].Text;
            }
        }

        private void btnInsertTabBill_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBillIDTabBill.Text.Trim()) ||
                String.IsNullOrEmpty(txtDateTabBill.Text.Trim()) ||
                String.IsNullOrEmpty(txtTotalPriceTabBill.Text.Trim()) ||
                String.IsNullOrEmpty(cboxShippingIDTabBill.Text.Trim()) ||
                String.IsNullOrEmpty(cboxStaffIDTabBill.Text.Trim())
                )
            {
                MessageBox.Show("NULL");
            }
            else
            {
                if (conn == null) conn = new SqlConnection(connetionString);
                if (conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                cmd.CommandText = "INSERT INTO bills(date,totalprice,shippingid,staffid)"
                    + "VALUES(@Date,@TotalPrice,@ShippingID,@StaffID)";
                SqlParameter parameter_Date = new SqlParameter("@Date", txtDateTabBill.Text);
                cmd.Parameters.Add(parameter_Date);
                SqlParameter parameter_TotalPriceD = new SqlParameter("@TotalPrice", txtTotalPriceTabBill.Text);
                cmd.Parameters.Add(parameter_TotalPriceD);
                SqlParameter parameter_ShippingID = new SqlParameter("@ShippingID", cboxShippingIDTabBill.Text);
                cmd.Parameters.Add(parameter_ShippingID);
                SqlParameter parameter_StaffID = new SqlParameter("@StaffID", cboxStaffIDTabBill.Text);
                cmd.Parameters.Add(parameter_StaffID);

                try
                {
                    result = cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert Success!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Insert Failed!!");
                }
                if (result > 0)
                {
                    ViewListBill();
                }
            }
        }

        private void btnUpdateTabBill_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBillIDTabBill.Text.Trim()) ||
                String.IsNullOrEmpty(txtDateTabBill.Text.Trim()) ||
                String.IsNullOrEmpty(txtTotalPriceTabBill.Text.Trim()) ||
                String.IsNullOrEmpty(cboxShippingIDTabBill.Text.Trim()) ||
                String.IsNullOrEmpty(cboxStaffIDTabBill.Text.Trim())
                )
            {
                MessageBox.Show("NULL");
            }
            else
            {
                if (conn == null) conn = new SqlConnection(connetionString);
                if (conn.State == ConnectionState.Closed) conn.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE bills SET date='{txtDateTabBill.Text}', totalprice='{txtTotalPriceTabBill.Text}', shippingid='{cboxShippingIDTabBill.Text}', staffid='{cboxStaffIDTabBill.Text}'" +
                    $" WHERE billid='{txtBillIDTabBill.Text}'", conn);

                try
                {
                    result = cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Success!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n Update Failed!!");
                }
                if (result > 0)
                {
                    ViewListBill();
                }
            }
        }

        private void btnDeleteTabBill_Click(object sender, EventArgs e)
        {
            if (conn == null) conn = new SqlConnection(connetionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlCommand cmd = new SqlCommand($"DELETE FROM [bills] WHERE billid='{txtBillIDTabBill.Text}'", conn);

            try
            {
                result = cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Success!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n Delete Failed!!");
            }
            if (result > 0)
            {
                ViewListBill();
                txtBillIDTabBill.Text = "";
                txtDateTabBill.Text = "";
                txtTotalPriceTabBill.Text = "";
                cboxShippingIDTabBill.Text = "";
                cboxStaffIDTabBill.Text = "";
            }
        }
    }
}
