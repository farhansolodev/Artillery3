﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using static ArtillerySeries.src.Utilities;

namespace ArtillerySeries.src
{
    public class UI_WeaponEquipBox : UIElement
    {

        /*
         * Box Layouts:
         * +---------------+   +------+
         * |   Main        |   | Move |
         * +---------------+   +------+
         */
        A3RData _a3RData;

        Color _mainBoxColor;
        Color _targetMainBoxColor;

        Color _moveBoxColor;
        Color _targetMoveBoxColor;

        Color _textColor;
        Color _targetTextColor;

        Color _highlightMainColor;
        Color _highlightMoveColor;
        Color _targetHighlightColor;

        private bool _mouseOverMain, _mouseOverMove;
        private bool _mouseSelectedMain, _mouseSelectedMove;
        private bool _isActive;

        Color _isActiveColor;

        private Rectangle _moveBox;
        private Rectangle _mainBox;

        private Rectangle _moveBoxActiveArea;
        private Rectangle _mainBoxActiveArea;


        private Weapon _heldWeapon;

        private int _padding = 10;

        public Weapon HeldWeapon { get => _heldWeapon; set => _heldWeapon = value; }
        public bool IsActive { get => _isActive; set => _isActive = value; }

        public UI_WeaponEquipBox(Camera camera, A3RData a3RData, Vector pos)
            : base(camera)
        {
            _a3RData = a3RData;
            Pos = pos;

            _mainBox = new Rectangle()
            {
                X = Pos.X + Camera.Pos.X,
                Y = Pos.Y + Camera.Pos.Y,
                Width = 220,
                Height = 50
            };

            _moveBox = new Rectangle()
            {
                X = _mainBox.X + _mainBox.Width + 20,
                Y = _mainBox.Y,
                Width = 30,
                Height = _mainBox.Height
            };

            _mainBoxActiveArea = new Rectangle()
            {
                X = Pos.X,
                Y = Pos.Y,
                Width = 220,
                Height = 50
            };

            _moveBoxActiveArea = new Rectangle()
            {
                X = _mainBoxActiveArea.X + _mainBoxActiveArea.Width + 20,
                Y = _mainBoxActiveArea.Y,
                Width = 30,
                Height = _mainBoxActiveArea.Height
            };



            _targetMainBoxColor = SwinGame.RGBAFloatColor(0.3f, 0.3f, 0.3f, 0.2f);
            _targetMoveBoxColor = SwinGame.RGBAFloatColor(0.3f, 0.3f, 0.3f, 0.2f);
            _targetTextColor = Color.White;
            _targetHighlightColor = Color.Pink;

            _isActiveColor = Color.Black;

            _mainBoxColor = SwinGame.RGBAFloatColor(0, 0, 0, 0);
            _moveBoxColor = SwinGame.RGBAFloatColor(0, 0, 0, 0);
            _textColor = SwinGame.RGBAFloatColor(0, 0, 0, 0);
            _highlightMainColor = SwinGame.RGBAFloatColor(0, 0, 0, 0);
            _highlightMoveColor = SwinGame.RGBAFloatColor(0, 0, 0, 0);


        }



        Color IncreaseBrightness(Color c, int increase)
        {
            return SwinGame.RGBAColor(
                (byte)Clamp(c.R + increase, 0, 255),
                (byte)Clamp(c.G + increase, 0, 255),
                (byte)Clamp(c.B + increase, 0, 255),
                (byte)Clamp(c.A + increase, 0, 255));
        }

        public override void Draw()
        {
            SwinGame.FillRectangle(_mainBoxColor, _mainBox);
            SwinGame.FillRectangle(_moveBoxColor, _moveBox);

            SwinGame.FillRectangle(_highlightMainColor, _mainBox);
            SwinGame.FillRectangle(_highlightMoveColor, _moveBox);

            if (_mouseOverMain || _mouseSelectedMain)
                SwinGame.DrawRectangle(_targetHighlightColor, _mainBox);

            if (_mouseOverMove || _mouseSelectedMove)
                SwinGame.DrawRectangle(_targetHighlightColor, _moveBox);

            if (_heldWeapon != null)
            {
                SwinGame.DrawText(_heldWeapon.Name.Substring(0, 26), IncreaseBrightness(_a3RData.RarityReference[_heldWeapon.Rarity], -20),
                    SwinGame.FontNamed("shopFont"), Pos.X + 10 + Camera.Pos.X, Pos.Y + 10 + Camera.Pos.Y);

                SwinGame.DrawText("Active: ", _textColor, SwinGame.FontNamed("smallFont"),
                    Pos.X + 10 + Camera.Pos.X, Pos.Y + 25 + Camera.Pos.Y);

                SwinGame.DrawText(_isActive.ToString(), _isActiveColor, SwinGame.FontNamed("smallFont"),
                    Pos.X + 60 + Camera.Pos.X, Pos.Y + 25 + Camera.Pos.Y);
            }
            else
            {
                SwinGame.DrawText("Empty Slot", Color.DarkGray,
                    SwinGame.FontNamed("shopFont"), Pos.X + 10 + Camera.Pos.X, Pos.Y + 10 + Camera.Pos.Y);
                SwinGame.DrawText("Inactive", Color.DarkGray, SwinGame.FontNamed("smallFont"),
                    Pos.X + 10 + Camera.Pos.X, Pos.Y + 25 + Camera.Pos.Y);
            }


        }

        public override void Update()
        {
            if (_heldWeapon == null)
            {
                _targetMainBoxColor = SwinGame.RGBAFloatColor(0.3f, 0.3f, 0.3f, 0.5f);
                _targetMoveBoxColor = SwinGame.RGBAFloatColor(0.3f, 0.3f, 0.3f, 0.5f);

            }
            else
            {
                _targetMainBoxColor = SwinGame.RGBAFloatColor(0.3f, 0.3f, 0.3f, 0.2f);
                _targetMoveBoxColor = SwinGame.RGBAFloatColor(0.3f, 0.3f, 0.3f, 0.2f);
            }

            if (_isActive)
                _isActiveColor = Color.LightGreen;
            else
                _isActiveColor = Color.Red;

            _mainBox.X = Camera.Pos.X + Pos.X;
            _mainBox.Y = Camera.Pos.Y + Pos.Y;
            _moveBox.X = _mainBox.X + _mainBox.Width + 20;
            _moveBox.Y = _mainBox.Y;

            _mouseOverMain = SwinGame.PointInRect(SwinGame.MousePosition(), _mainBoxActiveArea);
            _mouseOverMove = SwinGame.PointInRect(SwinGame.MousePosition(), _moveBoxActiveArea);

            if (SwinGame.MouseClicked(MouseButton.LeftButton))
            {
                if (_mouseOverMain)
                {
                    _highlightMainColor = _targetHighlightColor;
                    _mouseSelectedMain = !_mouseSelectedMain;
                }


                if (_mouseOverMove)
                {
                    _highlightMoveColor = _targetHighlightColor;
                    _mouseSelectedMove = !_mouseSelectedMove;
                }
            }

            _mainBoxColor = UpdateColor(_mainBoxColor, _targetMainBoxColor);
            _moveBoxColor = UpdateColor(_moveBoxColor, _targetMoveBoxColor);
            _highlightMainColor = UpdateColor(_highlightMainColor, _targetHighlightColor, -5);
            _highlightMoveColor = UpdateColor(_highlightMoveColor, _targetHighlightColor, -5);
            _textColor = UpdateColor(_textColor, _targetTextColor);

        }
    }
}
