namespace Templates.HardwareTest.Microphone
{
    /// <summary>
    /// Provides functionality to record sound and play it.
    /// </summary>
    public interface IMicrophoneService
    {
        /// <summary>
        /// Start recording file with given file name (without extension)
        /// </summary>
        /// <param name="fileName">File name without extension</param>
        void StartRecording(string fileName);
        /// <summary>
        /// Stop recording.
        /// </summary>
        void StopRecording();

        /// <summary>
        /// Play recorded sound.
        /// </summary>
        void PlayRecordedSound();

        /// <summary>
        /// Stop playing recorded sound.
        /// </summary>
        void StopPlaying();
    }
}