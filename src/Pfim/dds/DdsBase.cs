﻿using System;

namespace Pfim
{
    public abstract class DdsBase
    {
        protected DdsHeader Header { get; private set; } 

        private DdsLoadInfo _loadInfo;
        protected DdsLoadInfo LoadInfo
        {
            get
            {
                return _loadInfo;
            }
            set
            {
                _loadInfo = value;
                CalculateSizeInfo();
            }
        }

        public DdsBase(DdsHeader header)
        {
            Header = header;
        }
        public DdsBase(DdsHeader header, DdsLoadInfo loadinfo)
        {
            Header = header;
            LoadInfo = loadinfo;
        }

        private void CalculateSizeInfo()
        {
            Size = (int)(Math.Max(LoadInfo.divSize, Header.Width) / LoadInfo.divSize * Math.Max(LoadInfo.divSize, Header.Height) / LoadInfo.divSize * LoadInfo.blockBytes);
            //BitsPerPixel = ((int)LoadInfo.pixelFormat & 0xff00) >> 8;
            BytesPerPixel = (BitsPerPixel + 7) / 8;
            Stride = (int)(4 * ((Header.Width * BytesPerPixel + 3) / 4));
            NumberOfPixels = Header.Height * Header.Width * LoadInfo.divSize;
        }

        //public Bitmap Image { get; protected set;}

        public int Size { get; private set; }

        public int BitsPerPixel { get; private set; }

        public int BytesPerPixel { get; private set; }

        public int Stride { get; private set; }

        public uint NumberOfPixels { get; private set; }
    }
}