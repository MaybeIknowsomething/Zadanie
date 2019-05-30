using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Zadanie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Заполните все поля.", "Ошибка.");
            }
            else
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text; // Название
                dataGridView1.Rows[n].Cells[1].Value = textBox2.Text; // Цена   
                dataGridView1.Rows[n].Cells[2].Value = textBox4.Text; // Описание
                dataGridView1.Rows[n].Cells[3].Value = textBox3.Text; // Калорийность
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
                DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
                dt.TableName = "food"; // название таблицы
                dt.Columns.Add("Name"); // название колонок
                dt.Columns.Add("Price");// название колонок
                dt.Columns.Add("Description");// название колонок
                dt.Columns.Add("kkal");// название колонок
                ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше

                foreach (DataGridViewRow r in dataGridView1.Rows) // пока в dataGridView1 есть строки
                {
                    DataRow row = ds.Tables["food"].NewRow(); // создаем новую строку в таблице, занесенной в ds
                    row["Название"] = r.Cells[0].Value;  //в столбец этой строки заносим данные из первого столбца dataGridView1
                    row["Цена"] = r.Cells[1].Value; // то же самое со вторыми столбцами
                    row["Описание"] = r.Cells[2].Value; //то же самое с третьими столбцами
                    row["Калорийность"] = r.Cells[3].Value; //то же самое с четвертыми столбцами
                    ds.Tables["Food"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
                }
             
                    DataSet da = new DataSet();
                    da.WriteXml(@"D:\СиmSoft\Zadanie\\XMLFile1.xml");
                    MessageBox.Show("XML файл успешно сохранен.", "Выполнено.");  
            }
            catch
            {
        
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 1) //если в таблице больше одной строки
            {
                MessageBox.Show("Очистите поле перед загрузкой нового файла.", "Ошибка.");
            }
            else
            {
                if (File.Exists(@"D:\\XMLFile1.xml")) // если существует данный файл
                {
                DataSet ds = new DataSet(); // создаем новый пустой кэш данных
                ds.ReadXml(@"D:\\XMLFile1.xml"); // записываем в него XML-данные из файла

                foreach (DataRow item in ds.Tables["food"].Rows)
                    {
                        int n = dataGridView1.Rows.Add(); // добавляем новую сроку в dataGridView1
                        dataGridView1.Rows[n].Cells[0].Value = item["name"]; // заносим в первый столбец созданной строки данные из первого столбца таблицы ds.
                        dataGridView1.Rows[n].Cells[1].Value = item["price"]; // то же самое со вторым столбцом
                        dataGridView1.Rows[n].Cells[2].Value = item["description"]; // то же самое с третьим столбцом
                        dataGridView1.Rows[n].Cells[3].Value = item["calories"];
                    }
                }
                else
                {
                    MessageBox.Show("XML файл не найден.", "Ошибка.");
                }
        }

      
    }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[1].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[2].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[3].Cells[3].Value.ToString();
            int n = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
          
  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = textBox2.Text;
                dataGridView1.Rows[n].Cells[2].Value = textBox4.Text;
                dataGridView1.Rows[n].Cells[3].Value = textBox3.Text;
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.", "Ошибка.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index); //удаление
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблица пустая.", "Ошибка.");
            }
        }
}
}
