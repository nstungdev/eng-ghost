using EngGhost.Services;
using EngGhost.Views.Vocabulary;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EngGhost.Views.Pages
{
    /// <summary>
    /// Interaction logic for VocabularyOverview.xaml
    /// </summary>
    public partial class VocabularyOverview : Page
    {
        private readonly VocabularyService _vocabularyService;
        public VocabularyOverview()
        {
            InitializeComponent();

            _vocabularyService = new VocabularyService();
        }

        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            var vocabularies = await _vocabularyService.GetVocabulariesAsync();
            dtg_vocabularies.ItemsSource = vocabularies;
        }

        private void btn_createVocabulary_Click(object sender, RoutedEventArgs e)
        {
            var editForm = new VocabularyEditForm();
            editForm.ShowDialog();
        }
    }
}
