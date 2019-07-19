using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;
using CRUDSystem.Entities;
namespace CRUDSystem
{
    public partial class Form1 : Form
    {

        Detail MyDetail = new Detail();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopGridView();
        }

        private void PopGridView()
        {
            using (var MyModelEntities = new MyModel())
            {
                dataGridView1.DataSource = MyModelEntities.Details.ToList<Detail>();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                MyDetail.ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                using (var MyDBEntities = new MyModel())
                {
                    MyDetail = MyDBEntities.Details.Where(x => x.ID == MyDetail.ID).FirstOrDefault();
                    txtFName.Text = MyDetail.Fname;
                    txtLName.Text = MyDetail.Lname;
                    txtAge.Text = MyDetail.Age.ToString();
                    txtAddress.Text = MyDetail.Address;
                    dtDOB.Text = MyDetail.DOB.ToString();

                    btnSave.Text = "Update";
                   
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MyDetail.Fname = txtFName.Text;
            MyDetail.Lname = txtLName.Text;
            MyDetail.Age = Convert.ToInt32(txtAge.Text);
            MyDetail.Address = txtAddress.Text;
            MyDetail.DOB = Convert.ToDateTime(dtDOB.Text);

            using (var MyDbEntities = new MyModel())
            {

               if (MyDetail.ID == 0)
                {
                    MyDbEntities.Details.Add(MyDetail);
                    MyDbEntities.SaveChanges();

                    MessageBox.Show("Information has been Saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MyDbEntities.Entry(MyDetail).State = EntityState.Modified;
                    MyDbEntities.SaveChanges();

                    btnSave.Text = "Save";
                    MyDetail.ID = 0;
                    MessageBox.Show("Information has been Updated", "Modified", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
              
            }

            PopGridView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //MyDetail.Fname = txtFName.Text;
            //MyDetail.Lname = txtLName.Text;
            //MyDetail.Age = Convert.ToInt32(txtAge.Text);
            //MyDetail.Address = txtAddress.Text;
            //MyDetail.DOB = Convert.ToDateTime(dtDOB.Text);

            //using (var MyDbEntities = new MyModel())
            //{
               
            //}

            //PopGridView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete this information?", "Please Confirmed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {


                using (var MyDbEntities = new MyModel())
                {
                    var entry = MyDbEntities.Entry(MyDetail);
                    if (entry.State == EntityState.Detached)
                    {
                        MyDbEntities.Details.Attach(MyDetail);

                        MyDbEntities.Details.Remove(MyDetail);
                        MyDbEntities.SaveChanges();
                        PopGridView();
                        ClearFields();
                    }
                }

            }

        }

        void ClearFields()
        {
            txtFName.Text = "";
            txtLName.Text = "";
            txtAddress.Text = "";
            txtAge.Text = "";
            dtDOB.Text = DateTime.Now.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
