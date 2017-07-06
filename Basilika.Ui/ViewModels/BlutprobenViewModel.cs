using Basilika.Core;
using Basilika.Core.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Basilika.Ui.ViewModels
{
    public class BlutprobenViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private IEnumerable<Blutprobe> _blutproben;
        public IEnumerable<Blutprobe> Blutproben
        {
            get => _blutproben;
            set
            {
                _blutproben = value;
                RaisePropertyChanged();
            }
        }
        private Blutprobe _selectedBlutprobe;
        public Blutprobe SelectedBlutprobe
        {
            get => _selectedBlutprobe;
            set
            {
                _selectedBlutprobe = value;
                RaisePropertyChanged();
            }
        }

        public BlutprobenViewModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork ?? throw new ArgumentNullException();

        private ICommand _initializeCommand;
        public ICommand InitializeCommand => _initializeCommand ?? (_initializeCommand = new RelayCommand(
            async () => Blutproben = await _unitOfWork.Blutproben.GetAllOrderdDescendigByAsync(b => b.Datum)));

        private ICommand _loadUntersuchungenToSelectedBlutprobeCommand;
        public ICommand LoadUntersuchungenToSelectedBlutprobeCommand => _loadUntersuchungenToSelectedBlutprobeCommand ??
            (_loadUntersuchungenToSelectedBlutprobeCommand = new RelayCommand(
                () =>
                {
                    if (SelectedBlutprobe.Untersuchungen.Count() == 0)
                    {
                        _unitOfWork.Untersuchungen.Find(u => u.ProbeId == SelectedBlutprobe.Id);
                        RaisePropertyChanged(nameof(SelectedBlutprobe));
                    }
                }));
    }
}