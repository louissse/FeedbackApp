using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackApp.Repositories
{
    public interface ICatRepository
    {
        byte[] GetCatPhoto();
    }
}
