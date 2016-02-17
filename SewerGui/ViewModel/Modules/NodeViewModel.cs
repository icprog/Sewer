﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mod.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SewerGui.ViewModel
{
    public abstract class NodeViewModel : ViewModelBase
    {
        private IInitiator baseItem = null;

        private double top = 0;
        private double left = 0;
        private double right = 0;
        private double bottom = 0;
        private double rotation = 0;

        public IInitiator BaseItem { get { return baseItem; } set { baseItem = value; RaisePropertyChanged(() => BaseItem); } }

        public double Top { get { return top; } set { top = value; RaisePropertyChanged(() => Top); } }
        public double Left { get { return left; } set { left = value; RaisePropertyChanged(() => Left); } }
        public double Right { get { return right; } set { right = value; RaisePropertyChanged(() => Right); } }
        public double Bottom { get { return bottom; } set { bottom = value; RaisePropertyChanged(() => Bottom); } }

        public double Width { get { return Right - Left; } set { Right = Left + value; RaisePropertyChanged(() => Width); } }
        public double Height { get { return Bottom - Top; } set { Bottom = Top + value; RaisePropertyChanged(() => Height); } }

        public double Surface { get { return Width * Height; } }

        public double Rotation { get { return rotation; } set { rotation = value; RaisePropertyChanged(() => Rotation); } }

        public Point Center { get { return new Point(Left + (Right - Left) / 2.0f, Top + (Bottom - Top) / 2.0f); } }

        public bool IsInitialized { get { return BaseItem == null ? false : BaseItem.IsInitialized; } }
        public virtual Guid UniqueId { get { return BaseItem == null ? Guid.Empty : BaseItem.UniqueId; } }

        private ICommand initializeCommand = null;

        public ICommand InitializeCommand
        {
            get
            {
                if (initializeCommand == null)
                    initializeCommand = new RelayCommand(DoInitializeCommand, CanInitializeCommmand);
                return initializeCommand;
            }
        }

        private ICommand cleanupCommand = null;

        public ICommand CleanupCommand
        {
            get
            {
                if (cleanupCommand == null)
                    cleanupCommand = new RelayCommand(DoCleanupCommand, CanCleanupCommand);
                return cleanupCommand;
            }
        }

        public virtual void DoInitializeCommand()
        {
            if (BaseItem != null)
                BaseItem.Initialize();
        }

        public virtual bool CanInitializeCommmand()
        {
            if (BaseItem != null)
                return !IsInitialized;
            return false;
        }

        public virtual void DoCleanupCommand()
        {
            if (BaseItem != null)
                BaseItem.Cleanup();
        }

        public virtual bool CanCleanupCommand()
        {            
            return IsInitialized;
        }
        
    }
}