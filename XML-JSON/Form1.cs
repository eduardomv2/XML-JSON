using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace XML_JSON
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Guardar como JSON"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<Dictionary<string, object>> jsonData = new List<Dictionary<string, object>>();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (!dataGridView1.Rows[i].IsNewRow)
                    {
                        Dictionary<string, object> rowData = new Dictionary<string, object>();
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            string key = dataGridView1.Columns[j].HeaderText;
                            string value = dataGridView1.Rows[i].Cells[j].Value?.ToString() ?? "";
                            rowData[key] = value;
                        }
                        jsonData.Add(rowData);
                    }
                }

                string jsonString = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
                File.WriteAllText(saveFileDialog.FileName, jsonString);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                Title = "Abrir JSON"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string jsonString = File.ReadAllText(openFileDialog.FileName);
                List<Dictionary<string, object>> jsonData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonString);

                if (jsonData.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();

                    // Agregar encabezados
                    foreach (string header in jsonData[0].Keys)
                    {
                        dataGridView1.Columns.Add(header, header);
                    }

                    // Agregar filas
                    foreach (var row in jsonData)
                    {
                        dataGridView1.Rows.Add(row.Values.ToArray());
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML files (*.xml)|*.xml",
                Title = "Guardar como XML"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Dictionary<string, object>>));
                List<Dictionary<string, object>> xmlData = new List<Dictionary<string, object>>();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (!dataGridView1.Rows[i].IsNewRow)
                    {
                        Dictionary<string, object> rowData = new Dictionary<string, object>();
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            rowData[dataGridView1.Columns[j].HeaderText] = dataGridView1.Rows[i].Cells[j].Value?.ToString() ?? "";
                        }
                        xmlData.Add(rowData);
                    }
                }

                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    serializer.Serialize(fs, xmlData);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml",
                Title = "Abrir XML"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Dictionary<string, object>>));
                using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    List<Dictionary<string, object>> xmlData = (List<Dictionary<string, object>>)serializer.Deserialize(fs);

                    if (xmlData.Count > 0)
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();

                        // Agregar encabezados
                        foreach (string header in xmlData[0].Keys)
                        {
                            dataGridView1.Columns.Add(header, header);
                        }

                        // Agregar filas
                        foreach (var row in xmlData)
                        {
                            dataGridView1.Rows.Add(row.Values.ToArray());
                        }
                    }
                }

            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            //crea una columna y una fila
            dataGridView1.Columns.Add("Columna1", "Columna1");
        }
    }
}
