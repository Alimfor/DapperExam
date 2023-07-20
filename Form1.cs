using Dapper;
using Exam.DB;
using Exam.Model;
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

namespace Exam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void AddAuthor_Click(object sender, EventArgs e)
        {
            using (var db = (SqlConnection)GetConnection.getConnection)
            {
                var result = db.Execute("pInsertAuthor",new {firstName = tbFirstName.Text, lastName = tbLastName.Text},commandType: CommandType.StoredProcedure);
                MessageBox.Show("sacessful",result.ToString());
            }
        }

        private void AddCategory_Click(object sender, EventArgs e)
        {
            using (var db = (SqlConnection)GetConnection.getConnection)
            {
                var result = db.Execute("pInsertCategory", new { name = tbCategory.Text }, commandType: CommandType.StoredProcedure);
                MessageBox.Show("sacessful");
            }
        }

        private void AddBook_Click(object sender, EventArgs e)
        {
            using (var db = (SqlConnection)GetConnection.getConnection)
            {
                int author_id = db.QueryFirstOrDefault<Author>("pGetAuthorIdByFullName",
                                            new
                                            {
                                                firstName = tbAuthorFirstName.Text,
                                                lastName = tbAutorLastName.Text
                                            },
                                            commandType: CommandType.StoredProcedure)
                                            .id;

                int category_id = db.QueryFirstOrDefault<Category>("pGetCategoryIdByName",
                                             new { name = tbBookCategory.Text },
                                             commandType: CommandType.StoredProcedure)
                                            .id;


                var result = db.Execute("pInsertBook",
                                        new
                                        {
                                            title = tbBookName.Text,
                                            pages = nudPages.Value,
                                            cost = nudCost.Value,
                                            authorId = author_id,
                                            categoryId = category_id
                                        },
                                        commandType: CommandType.StoredProcedure);
                MessageBox.Show("sacessful",result.ToString());
            }
        }

        private void GetAllBook_Click(object sender, EventArgs e)
        {
            using (var db = (SqlConnection)GetConnection.getConnection)
            {
                var result = db.Query<Book>("pGetAllBook", commandType: CommandType.StoredProcedure);
                dgv.DataSource = result;
            }
        }

        private void GetBookById_Click(object sender, EventArgs e)
        {
            using (var db = (SqlConnection)GetConnection.getConnection)
            {
                var result = db.Query<Book>("pGetBookById", new { id = nudId.Value }, commandType: CommandType.StoredProcedure);
                dgv.DataSource = result;
            }
        }

        private void UpdateBook_Click(object sender, EventArgs e)
        {
            using (var db = (SqlConnection)GetConnection.getConnection)
            {
                int author_id = db.QueryFirstOrDefault<Author>("pGetAuthorIdByFullName",
                                            new
                                            {
                                                firstName = tbAuthorFirstName.Text,
                                                lastName = tbAutorLastName.Text
                                            },
                                            commandType: CommandType.StoredProcedure)
                                            .id;

                int category_id = db.QueryFirstOrDefault<Category>("pGetCategoryIdByName",
                                             new { name = tbBookCategory.Text },
                                             commandType: CommandType.StoredProcedure)
                                            .id;


                var result = db.Execute("pUpdateBook",
                                        new
                                        {
                                            id = nudId.Value,
                                            title = tbBookName.Text,
                                            pages = nudPages.Value,
                                            cost = nudCost.Value,
                                            authorId = author_id,
                                            categoryId = category_id
                                        },
                                        commandType: CommandType.StoredProcedure);

                MessageBox.Show("sacessful",result.ToString());
            }
        }

        private void DeleteBook_Click(object sender, EventArgs e)
        {
            using (var db = (SqlConnection)GetConnection.getConnection)
            {
                var result = db.Execute("pDeleteBook",new { id = nudId.Value }, commandType: CommandType.StoredProcedure);
                MessageBox.Show("sacessful", result.ToString());
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgv.Rows[e.RowIndex];

                nudId.Value = Convert.ToInt32(selectedRow.Cells["id"].Value);
                tbBookName.Text = selectedRow.Cells["title"].Value?.ToString();
                nudPages.Value = Convert.ToInt32(selectedRow.Cells["pages"].Value);
                nudCost.Value = Convert.ToInt32(selectedRow.Cells["cost"].Value);
                using (var db = (SqlConnection)GetConnection.getConnection)
                {
                    var fullName = db.QueryFirstOrDefault<Author>("pGetAuthorFullNameById",
                                                new { authorId = Convert.ToInt32(selectedRow.Cells["authorId"].Value) },
                                                commandType: CommandType.StoredProcedure);

                    tbAuthorFirstName.Text = fullName.firstName;
                    tbAutorLastName.Text = fullName.lastName;

                    tbBookCategory.Text = db.QueryFirstOrDefault<Category>("pGetCategoryNameById",
                                                                 new { categoryId = Convert.ToInt32(selectedRow.Cells["categoryId"].Value) },
                                                                 commandType: CommandType.StoredProcedure).name;
                }
            }
        }
    }
}
