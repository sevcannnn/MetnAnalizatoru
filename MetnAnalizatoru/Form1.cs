using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QeydlerWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = ofd.FileName;
                        textBox1.Text = filePath; 
                         
                        string content = File.ReadAllText(filePath);
                        string[] lines = File.ReadAllLines(filePath);

                        int lineCount = lines.Length;
                        int charCount = content.Length;

                        char[] delimiters = new char[] { ' ', '\r', '\n', '.', ',', '!', '?', ';', ':', '-', '(', ')' };
                        string[] words = content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                        int wordCount = words.Length;

                       
                        string longestWord = words.Length > 0 ? words.OrderByDescending(w => w.Length).FirstOrDefault() : "";

                     
                        Dictionary<string, int> wordFreq = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                        foreach (var w in words)
                        {
                            if (wordFreq.ContainsKey(w))
                                wordFreq[w]++;
                            else
                                wordFreq[w] = 1;
                        }

                 
                        var top3 = wordFreq.OrderByDescending(pair => pair.Value).Take(3).ToList();

                       
                        using (StreamWriter sw = new StreamWriter("hesabat.txt"))
                        {
                            sw.WriteLine("--- METN ANALIZ HESABATI ---");
                            sw.WriteLine($"Setir sayi: {lineCount}");
                            sw.WriteLine($"Soz sayi: {wordCount}");
                            sw.WriteLine($"Simvol sayi: {charCount}");
                            sw.WriteLine($"En uzun soz: {longestWord}");
                            sw.WriteLine("\nEn cox tekrarlanan 3 soz:");
                            foreach (var item in top3)
                            {
                                sw.WriteLine($"- {item.Key}: {item.Value} defe");
                            }
                        }

                        
                        listBox1.Items.Clear();
                        listBox1.Items.Add($"Setir sayi: {lineCount}");
                        listBox1.Items.Add($"Soz sayi: {wordCount}");
                        listBox1.Items.Add($"Simvol sayi: {charCount}");
                        listBox1.Items.Add($"En uzun soz: {longestWord}");
                        listBox1.Items.Add("--- En cox tekrarlanan 3 soz ---");
                        foreach (var item in top3)
                        {
                            listBox1.Items.Add($"{item.Key}: {item.Value} defe");
                        }

                        MessageBox.Show("Analiz tamamlandi ve neticeler 'hesabat.txt' faylina yazildi!", "Ugurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xeta bas verdi: {ex.Message}", "Xeta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}