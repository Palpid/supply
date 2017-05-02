using DevExpress.XtraEditors;
using HKSupply.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HKSupply.Helpers
{
    public class DocHelper
    {
        public static void OpenDoc(string fullFilePath)
        {
            try
            {
                //Validamos que el fichero exista
                if (System.IO.File.Exists(fullFilePath))
                {
                    string extension = System.IO.Path.GetExtension(fullFilePath);
                    switch (extension.ToUpper())
                    {
                        case ".PDF":
                            PDFViewer pdfViewer = new PDFViewer();
                            pdfViewer.pdfFile = fullFilePath;
                            pdfViewer.ShowDialog();
                            break;

                        case ".JPG":
                        case ".PNG":
                            using (Form form = new Form())
                            {
                                PictureBox pb = new PictureBox();
                                pb.Dock = DockStyle.Fill;
                                pb.BackgroundImageLayout = ImageLayout.Center;
                                pb.Image = Image.FromFile(fullFilePath);

                                form.Text = "Img Viewer";
                                form.Width = 1280;
                                form.Height = 720;
                                form.Controls.Add(pb);
                                form.ShowDialog();
                            }
                            break;

                        default:
                            XtraMessageBox.Show("File not supported.");
                            break;
                    }
                }
                else
                {
                    XtraMessageBox.Show("File doesn't exist or server is down or inaccessible", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
