﻿using ISynthSounds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LowNotesApp
{
    class MidNotes : ISynthPlugin
    {
        public string Name => "Mid notes";
        public string Description => "Plugin with low notes";
        public float C => 261.63f;
        public float C_S => 277.18f;
        public float D => 293.66f;
        public float D_S => 311.13f;
        public float E => 329.63f;
        public float F => 349.23f;
        public float F_S => 369.99f;
        public float G => 392.00f;
        public float G_S => 415.30f;
        public float A => 440.00f;
        public float A_S => 466.16f;
        public float B => 493.88f;

        public void PlayNote(float f)
        {
            const int SampleRate = 44100;
            const short BitsPerSample = 16;

            short[] wave = new short[SampleRate];
            byte[] bynaryWave = new byte[SampleRate * sizeof(short)];
            float frequency = f;

            for (int i = 0; i < SampleRate; i++)
            {
                wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * frequency) / SampleRate) * i));
            }

            Buffer.BlockCopy(wave, 0, bynaryWave, 0, wave.Length * sizeof(short));

            using (MemoryStream memoryStream = new MemoryStream())
            using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
            {
                // making a wave format file
                int blockAlign = BitsPerSample / 8;
                int subChunkTwoSize = SampleRate * blockAlign;
                binaryWriter.Write(new[] { 'R', 'I', 'F', 'F' });
                binaryWriter.Write(36 + subChunkTwoSize);
                binaryWriter.Write(new[] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });
                binaryWriter.Write(16);
                binaryWriter.Write((short)1);
                binaryWriter.Write((short)1);
                binaryWriter.Write(SampleRate);
                binaryWriter.Write(SampleRate * blockAlign);
                binaryWriter.Write((short)blockAlign);
                binaryWriter.Write(BitsPerSample);
                binaryWriter.Write(new[] { 'd', 'a', 't', 'a' });
                binaryWriter.Write(subChunkTwoSize);
                binaryWriter.Write(bynaryWave);
                memoryStream.Position = 0;
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer(memoryStream);
                sp.Play();
                //new SoundPlayer(memoryStream).Play();
            }
        }
    }
}
