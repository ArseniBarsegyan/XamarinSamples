namespace Templates.HardwareTest.Sound
{
    /// <summary>
    /// Provides functionality to play sound.
    /// </summary>
    public interface IAudioService
    {
        void PlayAudioFile(string fileName);
        void StopPlaying();
    }
}