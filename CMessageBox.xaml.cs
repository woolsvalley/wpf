using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BanzoomaOEPv4
{
    /// <summary>
    /// Interaction logic for CMessageBox.xaml
    /// </summary>
    public partial class CMessageBox : Window
    {
        public CMessageBox()
        {
            InitializeComponent();
        }
        static CMessageBox cMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public CMessageTitle messageTitle { get; set; }
        public enum CMessageButton
        {
            Ok,
            Yes,
            No,
            Cancel,
            Confirm

        }
        public string GetTitle(CMessageTitle value)
        {
            return Enum.GetName(typeof(CMessageTitle), value);
        }
        public string GetButtonText(CMessageButton value)
        {
            return Enum.GetName(typeof(CMessageButton), value);
        }
      
        public enum CMessageTitle
        {
            Error,
            Info,
            Warning,
            Confirm
        }
        public static DialogResult Show(string message,CMessageTitle title,CMessageButton okButton,CMessageButton noButton)
        {
            cMessageBox = new CMessageBox();
            cMessageBox.btnOk.Content = cMessageBox.GetButtonText(okButton);
            cMessageBox.btnCancel.Content = cMessageBox.GetButtonText(noButton);
            cMessageBox.txtMessage.Text = message;
            cMessageBox.txtTitle.Text = cMessageBox.GetTitle(title);
            
            //icon
            switch (title)
            {
                case CMessageTitle.Error:
                    cMessageBox.msgLogo.Kind = PackIconKind.Error;
                    cMessageBox.msgLogo.Foreground = Brushes.DarkRed;
                    break;
                case CMessageTitle.Info:
                    cMessageBox.msgLogo.Kind = PackIconKind.InfoCircle;
                    cMessageBox.msgLogo.Foreground = Brushes.Blue;
                    cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageTitle.Warning:
                    cMessageBox.msgLogo.Kind = PackIconKind.Warning;
                    cMessageBox.msgLogo.Foreground = Brushes.Yellow;
                    cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageTitle.Confirm:
                    cMessageBox.msgLogo.Kind = PackIconKind.QuestionMark;
                    cMessageBox.msgLogo.Foreground = Brushes.Gray;
                    break;
            }
            cMessageBox.ShowDialog();
            return result;
        }
       
        private void BntOk_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            Border border = new Border();
            
            cMessageBox.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            cMessageBox.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Storyboard storyboard = new Storyboard();

            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            this.RenderTransformOrigin = new Point(0.5, 0.5);
            this.RenderTransform = scale;

            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(300);
            growAnimation.From = 0;
            growAnimation.To = 1;
            storyboard.Children.Add(growAnimation);

            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growAnimation, this);

            storyboard.Begin();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            Closing -= Window_Closing;
            e.Cancel = true;
            Storyboard storyboard = new Storyboard();

            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            this.RenderTransformOrigin = new Point(0.5, 0.5);
            this.RenderTransform = scale;

            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(300);
            growAnimation.From = 1;
            growAnimation.To = 0;
            storyboard.Children.Add(growAnimation);

            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growAnimation, this);
            growAnimation.Completed += GrowAnimation_Completed;

            storyboard.Begin();
        }

        private void GrowAnimation_Completed(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
