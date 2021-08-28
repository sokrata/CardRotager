﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CardRotager {

    public class ImageRotater : PictureBox {
            private Bitmap _bitmap;
        private float _angle;

        public Bitmap Image {
            get { return _bitmap; }
            set {
                _bitmap = value;

                if (_bitmap is null) {
                    return;
                }

                Invalidate(); // для вызова метода отрисовки OnPaint(...).
            }
        }

        public float Angle {
            get { return _angle; }
            set {
                _angle = value;

                if (_bitmap is null) {
                    return;
                }

                Invalidate(); // для вызова метода отрисовки OnPaint(...).
            }
        }

        protected override void Dispose(bool disposing) {
            Image?.Dispose();

            if (disposing) {
                Image = null;
                Angle = .0f;
            }

            base.Dispose(disposing);
        }

        public ImageRotater() {
            DoubleBuffered = true; // что бы не было мерцаний.
        }

        protected override void OnPaint(PaintEventArgs e) {
            e.Graphics.Clear(BackColor); // можно изменить на любой другой, я использую фоновый цвет.

            if (Image is null) {
                base.OnPaint(e);
                return;
            }

            e.Graphics.TranslateTransform(Image.Width + .0f, Image.Height + .0f);
            e.Graphics.RotateTransform(Angle);
            e.Graphics.TranslateTransform(-Image.Width + .0f, -Image.Height + .0f);
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImage(Image, new Point(0, 0));

            base.OnPaint(e);
        }

        //    private Image _image;

        //    //Double buffer the control
        //    public ImageRotater() {
        //        this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        //        this.AutoScroll = true;
        //        this.Image = null;
        //        this.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        //        this.Zoom = 1f;
        //    }
        //    //New
        //    [Category("Appearance"), Description("The image to be displayed")]
        //    public Image Image {
        //        get { return _image; }
        //        set {
        //            _image = value;
        //            UpdateScaleFactor();
        //            Invalidate();
        //        }
        //    }
        //    private float _zoom = 1f;
        //    [Category("Appearance"), Description("The zoom factor. Less than 1 to reduce. More than 1 to magnify.")]
        //    public float Zoom {
        //        get { return _zoom; }
        //        set {
        //            if (value < 0 || value < 1E-05) {
        //                value = 1E-05f;
        //            }
        //            _zoom = value;
        //            UpdateScaleFactor();
        //            Invalidate();
        //        }
        //    }
        //    private void UpdateScaleFactor() {
        //        if (_image == null) {
        //            this.AutoScrollMargin = this.Size;
        //        } else {
        //            this.AutoScrollMinSize = new Size(Convert.ToInt32(this._image.Width * _zoom + 0.5f), Convert.ToInt32(this._image.Height * _zoom + 0.5f));
        //        }
        //    }
        //    //UpdateScaleFactor
        //    private InterpolationMode _interpolationMode = InterpolationMode.High;
        //    [Category("Appearance"), Description("The interpolation mode used to smooth the drawing")]
        //    public InterpolationMode InterpolationMode {
        //        get { return _interpolationMode; }
        //        set { _interpolationMode = value; }
        //    }
        //    protected override void OnPaintBackground(PaintEventArgs pevent) {
        //    }
        //    //OnPaintBackground
        //    protected override void OnPaint(PaintEventArgs e) {
        //        //if no image, don't bother. I tried check for IsNothing(_image) but this test wasn't detecting a no-image.
        //        if (_image == null) {
        //            base.OnPaint(e);
        //            return;
        //        }
        //        //Added because the first test sometimes failed
        //        try {
        //            int H = _image.Height;
        //            //Throws an exception if image is nothing.
        //        } catch (Exception ex) {
        //            base.OnPaint(e);
        //            //Debug.WriteLine(ex.ToString());
        //            return;
        //        }
        //        //Set up a zoom matrix
        //        Matrix mx = new Matrix(_zoom, 0, 0, _zoom, 0, 0);
        //        mx.Translate(this.AutoScrollPosition.X / _zoom, this.AutoScrollPosition.Y / _zoom);
        //        e.Graphics.Transform = mx;
        //        e.Graphics.InterpolationMode = _interpolationMode;
        //        e.Graphics.DrawImage(_image, new Rectangle(0, 0, this._image.Width, this._image.Height), 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel);
        //        base.OnPaint(e);
        //    }
        //    private Bitmap _bitmap;
        //    private float _angle;

        //    public Bitmap Bitmap {
        //        get { return _bitmap; }
        //        set {
        //            _bitmap = value;

        //            if (_bitmap is null) {
        //                return;
        //            }

        //            Invalidate(); // для вызова метода отрисовки OnPaint(...).
        //        }
        //    }

        //    public float Angle {
        //        get { return _angle; }
        //        set {
        //            _angle = value;

        //            if (_bitmap is null) {
        //                return;
        //            }

        //            Invalidate(); // для вызова метода отрисовки OnPaint(...).
        //        }
        //    }

        //    protected override void Dispose(bool disposing) {
        //        Bitmap?.Dispose();

        //        if (disposing) {
        //            Bitmap = null;
        //            Angle = .0f;
        //        }

        //        base.Dispose(disposing);
        //    }

        //    public ImageRotater() {
        //        DoubleBuffered = true; // что бы не было мерцаний.
        //    }

        //    protected override void OnPaint(PaintEventArgs e) {
        //        e.Graphics.Clear(BackColor); // можно изменить на любой другой, я использую фоновый цвет.

        //        if (Bitmap is null) {
        //            base.OnPaint(e);
        //            return;
        //        }

        //        e.Graphics.TranslateTransform(Bitmap.Width + .0f, Bitmap.Height + .0f);
        //        e.Graphics.RotateTransform(Angle);
        //        e.Graphics.TranslateTransform(-Bitmap.Width + .0f, -Bitmap.Height + .0f);
        //        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        e.Graphics.DrawImage(Bitmap, new Point(0, 0));

        //        base.OnPaint(e);
        //    }
    }
}
