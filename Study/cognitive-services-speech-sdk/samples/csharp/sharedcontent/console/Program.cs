//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace MicrosoftSpeechSDKSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //Outputter
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(950);
            //Console.OutputEncoding =Encoding.Unicode;
            //Console.OutputEncoding = Encoding.UTF8;
            
            Console.WriteLine("����r");
            Console.WriteLine("-----------------[�y�����r]------------------------");
            Console.WriteLine("1. Speech recognition with microphone input.");
            Console.WriteLine("2. Speech recognition in the specified language and using detailed output format.");
            Console.WriteLine("3. Speech continuous recognition with file input.");
            Console.WriteLine("4. Speech recognition using customized model.");
            Console.WriteLine("5. Speech recognition with audio stream.");
            Console.WriteLine("X. ���򪺤�����J����J");
            Console.WriteLine("-----------------[½Ķ�ഫ]------------------------");
            Console.WriteLine("6. Translation with microphone input.");
            Console.WriteLine("7. Translation with file input.");
            Console.WriteLine("8. Translation with audio stream.");
            Console.WriteLine("9. Speech continuous recognition using authorization token.");
            Console.WriteLine("-----------------[�y�����R]------------------------");
            Console.WriteLine("A. Intent recognition with microphone input.");
            Console.WriteLine("B. Intent continuous recognition with file input.");
            Console.WriteLine("C. Intent recognition in the specified language with microphone input.");

            Console.Write("Your choice (0: Stop.): ");

            ConsoleKeyInfo x;
            do
            {
                x = Console.ReadKey();
                Console.WriteLine("");
                switch (x.Key)
                {
                    case ConsoleKey.D1:
                        SpeechRecognitionSamples.RecognitionWithMicrophoneAsync().Wait();
                        break;
                    case ConsoleKey.D2:
                        SpeechRecognitionSamples.RecognitionWithLanguageAndDetailedOutputAsync().Wait();
                        break;
                    case ConsoleKey.D3:
                        SpeechRecognitionSamples.ContinuousRecognitionWithFileAsync().Wait();
                        break;
                    case ConsoleKey.D4:
                        SpeechRecognitionSamples.RecognitionUsingCustomizedModelAsync().Wait();
                        break;
                    case ConsoleKey.D5:
                        SpeechRecognitionSamples.RecognitionWithAudioStreamAsync().Wait();
                        break;
                    case ConsoleKey.X:
                        SpeechRecognitionSamples.ContinuousRecognitionWithMicrophoneAsync().Wait();
                        break;

                    case ConsoleKey.D6:
                        TranslationSamples.TranslationWithMicrophoneAsync().Wait();
                        break;
                    case ConsoleKey.D7:
                        TranslationSamples.TranslationWithFileAsync().Wait();
                        break;
                    case ConsoleKey.D8:
                        TranslationSamples.TranslationWithAudioStreamAsync().Wait();
                        break;
                    case ConsoleKey.D9:
                        SpeechRecognitionWithTokenSample.ContinuousRecognitionWithAuthorizationTokenAsync().Wait();
                        break;
                    case ConsoleKey.A:
                        IntentRecognitionSamples.RecognitionWithMicrophoneAsync().Wait();
                        break;
                    case ConsoleKey.B:
                        IntentRecognitionSamples.ContinuousRecognitionWithFileAsync().Wait();
                        break;
                    case ConsoleKey.C:
                        IntentRecognitionSamples.RecognitionWithMicrophoneUsingLanguageAsync().Wait();
                        break;
                    case ConsoleKey.D0:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
                Console.WriteLine("\nRecognition done. Your Choice (0: Stop): ");
            } while (x.Key != ConsoleKey.D0);
        }
    }
}
