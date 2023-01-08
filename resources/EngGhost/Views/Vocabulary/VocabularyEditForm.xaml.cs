using EngGhost.FormModels;
using EngGhost.Helpers;
using EngGhost.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EngGhost.Views.Vocabulary
{
    /// <summary>
    /// Interaction logic for VocabularyEditForm.xaml
    /// </summary>
    public partial class VocabularyEditForm : Window
    {
        private readonly VocabularyService _vocabularyService;
        public VocabularyEditForm()
        {
            _vocabularyService = new VocabularyService();
            InitializeComponent();
        }

        private async void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            var form = new VocabularyForm
            {
                Mean = inp_Mean.Text,
                Note = inp_Note.Text,
                Pronounce = inp_Pronounce.Text,
                WordType = inp_WordType.Text,
                Word = inp_Word.Text
            };

            try
            {
                // start process
                pcb_Loading.Visibility = Visibility.Visible;
                TriggerControlWhenLoading(true);
                await _vocabularyService.CreateOne(form);
                MessageBox.Show("Tạo mới thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tạo mới thất bại " + ex.Message);
            }
            finally
            {
                TriggerControlWhenLoading(false);
                pcb_Loading.Visibility = Visibility.Hidden;
            }
        }

        public void TriggerControlWhenLoading(bool isLoading)
        {
            var textBoxControls = WpfControlHelper.FindVisualChilds<TextBox>(this)
                .Where(e => e.Name.StartsWith("inp_"))
                .ToList();

            var buttonControls = WpfControlHelper.FindVisualChilds<Button>(this)
                .Where(e => e.Name.StartsWith("btn_"))
                .ToList();

            textBoxControls.ForEach(e => e.IsEnabled = !isLoading);
            buttonControls.ForEach(e => e.IsEnabled = !isLoading);
        }
    }
}
