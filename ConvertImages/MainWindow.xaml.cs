using System;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ConvertImages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var filePath = dialog.SelectedPath;

                

                var searchOptions = SearchOption.TopDirectoryOnly;
                if (cbSubDirectories.IsChecked == true)
                {
                    searchOptions = SearchOption.AllDirectories;
                }

                var files = Directory.GetFiles(filePath, txtSearchPattern.Text, searchOptions);
                foreach (var file in files)
                {
                    var fi = new FileInfo(file);
                    var name = fi.Name.Replace(fi.Extension, "");

                    using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        try
                        {
                            using (Image original = Image.FromStream(fs))
                            {
                                if (cbFileType.SelectedItem != null)
                                {
                                    var item = cbFileType.SelectedItem as System.Windows.Controls.ComboBoxItem;

                                    var ext = item.Content;
                                    ImageFormat format = ImageFormat.Png;
                                    switch (ext)
                                    {
                                        case "bmp":
                                            format = ImageFormat.Bmp;
                                            break;
                                        case "emf":
                                            format = ImageFormat.Emf;
                                            break;
                                        case "wmf":
                                            format = ImageFormat.Wmf;
                                            break;
                                        case "gif":
                                            format = ImageFormat.Gif;
                                            break;
                                        case "jpeg":
                                            format = ImageFormat.Jpeg;
                                            break;
                                        case "png":
                                            format = ImageFormat.Png;
                                            break;
                                        case "tiff":
                                            format = ImageFormat.Tiff;
                                            break;
                                        case "exif":
                                            format = ImageFormat.Exif;
                                            break;
                                        case "icon":
                                            format = ImageFormat.Icon;
                                            break;
                                    }
                                    
                                    original.Save(fi.Directory.FullName + "\\" + name + "." + ext, format);
                                }
                                else
                                {
                                    MessageBox.Show("Select at least one option");
                                }
                            }
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show(file + " is not valid for converting");
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.Message);
                        }
                    }

                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception) { }
                }
                MessageBox.Show("Completed!");
            }
        }
    }
}
