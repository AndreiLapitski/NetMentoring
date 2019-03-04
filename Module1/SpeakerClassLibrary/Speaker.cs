using System;

namespace SpeakerClassLibrary
{
    public static class Speaker
    {
        public static string SayHelloNow(string name)
        {
            string phrase = $"{DateTime.Now:h:mm tt}: Hello, {name}!";
            return phrase;
        }
    }
}
