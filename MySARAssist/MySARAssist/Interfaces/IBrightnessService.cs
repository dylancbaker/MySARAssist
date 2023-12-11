using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.Interfaces
{
    public interface IBrightnessService
    {
        void SetBrightness(float factor);
        float GetBrightness();
    }
}
