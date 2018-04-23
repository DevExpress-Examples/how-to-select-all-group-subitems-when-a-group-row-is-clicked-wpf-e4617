using System.Windows;

namespace dxSample {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataContext = SampleDataRow.CreateRows();
        }
    }
}
