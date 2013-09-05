using Windows.UI.Xaml.Controls;

namespace Kulman.WinRT.UI
{
    /// <summary>
    /// Grid control that can be used as a child of SemanticZoom
    /// </summary>
    public class SemanticGrid: Grid,ISemanticZoomInformation
    {
        public void CompleteViewChange()
        {
            //throw new NotImplementedException();
        }

        public void CompleteViewChangeFrom(SemanticZoomLocation source, SemanticZoomLocation destination)
        {
            //throw new NotImplementedException();
        }

        public void CompleteViewChangeTo(SemanticZoomLocation source, SemanticZoomLocation destination)
        {
            //throw new NotImplementedException();
        }

        public void InitializeViewChange()
        {
            //throw new NotImplementedException();
        }

        public bool IsActiveView
        {
            get;
            set;
        }

        public bool IsZoomedInView
        {
            get;
            set;
        }

        public void MakeVisible(SemanticZoomLocation item)
        {
            //throw new NotImplementedException();
        }

        public SemanticZoom SemanticZoomOwner
        {
            get;
            set;
        }

        public void StartViewChangeFrom(SemanticZoomLocation source, SemanticZoomLocation destination)
        {
            //throw new NotImplementedException();
        }

        public void StartViewChangeTo(SemanticZoomLocation source, SemanticZoomLocation destination)
        {
            //throw new NotImplementedException();
        }
    }
}
