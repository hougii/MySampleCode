//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

// <toplevel>
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
// </toplevel>

namespace MicrosoftSpeechSDKSamples
{
    public class SpeechRecognitionSamples
    {
        /// <summary>
        /// 統一Config訂閱資訊
        /// </summary>
        static SpeechConfig _config = SpeechConfig.FromSubscription("2f88adfcfc824858a77608090c25daa1", "westus"); 

        // Speech recognition from microphone.
        public static async Task RecognitionWithMicrophoneAsync()
        {
            // <recognitionWithMicrophone>
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            // The default language is "en-us".
            var config = _config;

            // Creates a speech recognizer using microphone as audio input.
            using (var recognizer = new SpeechRecognizer(config))
            {
                // Starts recognizing.
                Console.WriteLine("Say something...");

                // Performs recognition. RecognizeOnceAsync() returns when the first utterance has been recognized,
                // so it is suitable only for single shot recognition like command or query. For long-running
                // recognition, use StartContinuousRecognitionAsync() instead.
                var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    Console.WriteLine($"RECOGNIZED: Text={result.Text}");
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                }
            }
            // </recognitionWithMicrophone>
        }

        // Speech recognition in the specified spoken language and uses detailed output format.
        public static async Task RecognitionWithLanguageAndDetailedOutputAsync()
        {
            // <recognitionWithLanguageAndDetailedOutputFormat>
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            var config = _config;
            //var config = SpeechConfig.FromSubscription("YourSubscriptionKey", "YourServiceRegion");

            // Replace the language with your language in BCP-47 format, e.g., en-US.
            var language = "zh-TW";
            config.SpeechRecognitionLanguage = language;
            config.OutputFormat = OutputFormat.Detailed;

            // Creates a speech recognizer for the specified language, using microphone as audio input.
            // Requests detailed output format.
            using (var recognizer = new SpeechRecognizer(config))
            {
                // Starts recognizing.
                Console.WriteLine($"Say something in {language} ...");

                // Performs recognition. RecognizeOnceAsync() returns when the first utterance has been recognized,
                // so it is suitable only for single shot recognition like command or query. For long-running
                // recognition, use StartContinuousRecognitionAsync() instead.
                var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    //Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine($"RECOGNIZED: Text={result.Text}");
                    Console.WriteLine("  DETAILED RESULTS:");

                    var detailedResults = result.Best();
                    foreach (var item in detailedResults) // NOTE: We need to put this in all languages, or take it out of CSharp
                    {
                        Console.WriteLine($"    Confidence: {item.Confidence}, Text: {item.Text}, LexicalForm: {item.LexicalForm}, NormalizedForm: {item.NormalizedForm}, MaskedNormalizedForm: {item.MaskedNormalizedForm}");
                    }
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                }
            }
            // </recognitionWithLanguageAndDetailedOutputFormat>
        }

        // Speech recognition using a customized model.
        public static async Task RecognitionUsingCustomizedModelAsync()
        {
            // <recognitionCustomized>
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            var config = _config;
            // Replace with the CRIS endpoint id of your customized model.
            config.EndpointId = "YourEndpointId";

            // Creates a speech recognizer using microphone as audio input.
            using (var recognizer = new SpeechRecognizer(config))
            {
                Console.WriteLine("Say something...");

                // Performs recognition. RecognizeOnceAsync() returns when the first utterance has been recognized,
                // so it is suitable only for single shot recognition like command or query. For long-running
                // recognition, use StartContinuousRecognitionAsync() instead.
                var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

                // Checks results.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    Console.WriteLine($"RECOGNIZED: Text={result.Text}");
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                    }
                }
            }
            // </recognitionCustomized>
        }

        // Continuous speech recognition.
        public static async Task ContinuousRecognitionWithFileAsync()
        {
            // <recognitionContinuousWithFile>
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            var config = _config;

            var stopRecognition = new TaskCompletionSource<int>();

            // Creates a speech recognizer using file as audio input.
            // Replace with your own audio file name.
            using (var audioInput = AudioConfig.FromWavFileInput(@"whatstheweatherlike.wav"))
            {
                using (var recognizer = new SpeechRecognizer(config, audioInput))
                {
                    // Subscribes to events.
                    recognizer.Recognizing += (s, e) =>
                    {
#if true //原始寫每一行的
                        Console.WriteLine($"RECOGNIZING: Text={e.Result.Text}");
#endif
#if false //改寫為一直over write
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write($"RECOGNIZING: Text={e.Result.Text}");
#endif
                    };

                    recognizer.Recognized += (s, e) =>
                    {
                        Console.WriteLine();
                        if (e.Result.Reason == ResultReason.RecognizedSpeech)
                        {
                            Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");
                        }
                        else if (e.Result.Reason == ResultReason.NoMatch)
                        {
                            Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                        }
                    };

                    recognizer.Canceled += (s, e) =>
                    {
                        Console.WriteLine($"CANCELED: Reason={e.Reason}");

                        if (e.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }

                        stopRecognition.TrySetResult(0);
                    };

                    recognizer.SessionStarted += (s, e) =>
                    {
                        Console.WriteLine("\n    Session started event.");
                    };

                    recognizer.SessionStopped += (s, e) =>
                    {
                        Console.WriteLine("\n    Session stopped event.");
                        Console.WriteLine("\nStop recognition.");
                        stopRecognition.TrySetResult(0);
                    };

                    // Starts continuous recognition. Uses StopContinuousRecognitionAsync() to stop recognition.
                    await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                    // Waits for completion.
                    // Use Task.WaitAny to keep the task rooted.
                    Task.WaitAny(new[] { stopRecognition.Task });

                    // Stops recognition.
                    await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                }
            }
            // </recognitionContinuousWithFile>
        }

        // Speech recognition with audio stream
        public static async Task RecognitionWithAudioStreamAsync()
        {
            // <recognitionAudioStream>
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            var config = _config;

            var stopRecognition = new TaskCompletionSource<int>();

            // Create an audio stream from a wav file.
            // Replace with your own audio file name.
            using (var audioInput = Helper.OpenWavFile(@"whatstheweatherlike.wav"))
            {
                // Creates a speech recognizer using audio stream input.
                using (var recognizer = new SpeechRecognizer(config, audioInput))
                {
                    // Subscribes to events.
                    recognizer.Recognizing += (s, e) =>
                    {
                        Console.WriteLine($"RECOGNIZING: Text={e.Result.Text}");
                    };

                    recognizer.Recognized += (s, e) =>
                    {
                        if (e.Result.Reason == ResultReason.RecognizedSpeech)
                        {
                            Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");
                        }
                        else if (e.Result.Reason == ResultReason.NoMatch)
                        {
                            Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                        }
                    };

                    recognizer.Canceled += (s, e) =>
                    {
                        Console.WriteLine($"CANCELED: Reason={e.Reason}");

                        if (e.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }

                        stopRecognition.TrySetResult(0);
                    };

                    recognizer.SessionStarted += (s, e) =>
                    {
                        Console.WriteLine("\nSession started event.");
                    };

                    recognizer.SessionStopped += (s, e) =>
                    {
                        Console.WriteLine("\nSession stopped event.");
                        Console.WriteLine("\nStop recognition.");
                        stopRecognition.TrySetResult(0);
                    };

                    // Starts continuous recognition. Uses StopContinuousRecognitionAsync() to stop recognition.
                    await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                    // Waits for completion.
                    // Use Task.WaitAny to keep the task rooted.
                    Task.WaitAny(new[] { stopRecognition.Task });

                    // Stops recognition.
                    await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                }
            }
            // </recognitionAudioStream>
        }

        //用麥克風可以持續的講話(and output)，一直到關鍵字後才停止
        public static async Task ContinuousRecognitionWithMicrophoneAsync() {
            var config = _config;
            config.SpeechRecognitionLanguage = "zh-TW";
            var stopFlag = new TaskCompletionSource<int>();
            int count = 0;
            using (var recognizer = new SpeechRecognizer(config)) {
                
                #region Register Events
                //解析中
                recognizer.Recognizing += (s, e) =>
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write($"RECOGNIZING: Text={e.Result.Text}");
                };
                
                //解析完成
                recognizer.Recognized += (s, e) =>
                {
                    Console.WriteLine();
                    if (e.Result.Reason == ResultReason.RecognizedSpeech)
                    {
                        Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");
                    }
                    else if (e.Result.Reason == ResultReason.NoMatch)
                    {
                        Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                    }
                    if (count == 10) {
                        stopFlag.TrySetResult(0);
                    }
                    count++;
                };
                recognizer.Canceled += (s, e) =>
                {
                    Console.WriteLine($"CANCELED: Reason={e.Reason}");
                };

                recognizer.SessionStarted += (s, e) =>
                {
                    Console.WriteLine("\n    Session started event.");
                    Console.WriteLine("\n    可開始說話.....");
                };

                recognizer.SessionStopped += (s, e) =>
                {
                    Console.WriteLine("\n    Session stopped event.");
                };
                #endregion

                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);
                Task.WaitAny(new[] { stopFlag.Task });//??當有值時就會停止?
                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
            }

        }
    }
}
