using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
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

namespace TestWPFApplication
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

        SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        ManualResetEvent _manualreset = null;

        private void btnSpeak_Click(object sender, RoutedEventArgs e)
        {
            _manualreset = new ManualResetEvent(false);
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("text")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("exit")));
            _recognizer.SpeechRecognized += _recognizer_SpeechRecognized;
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            _recognizer.Dispose();
        }

        private void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Result.Text))
            {
                txtSpokenText.Text = "Found Text: "+ e.Result.Text;
            }
            else if (e.Result.Text=="exit")
            {
                _manualreset.Set();
            }
        }
    }
}
