using System;

namespace com.xavi.LoadingSpinnerModule
{
    public enum SpinnerType
    {
        SIMPLE
    }

    public interface ILoadingSpinnerSystem
    {
        void StartSpinner (SpinnerType spinnerType);
        void StopSpinner ();
    }
}

