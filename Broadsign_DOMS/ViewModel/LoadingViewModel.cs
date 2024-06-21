using Broadsign_DOMS.Model;
using Broadsign_DOMS.Service;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Broadsign_DOMS.ViewModel
{
    public class LoadingViewModel : ObservableObject, IPageViewModel
    {
        private readonly BroadsignService _broadsignService;
        private ObservableCollection<Domain> _listDomains;
        private Domain _currentDomain;
        private IDictionary<Type, IList> _typeList;
        private string _loadingMessage;

        public LoadingViewModel()
        {
            _broadsignService = BroadsignService.Instance;
            _initializeTypeList();
            _ = _loadDataAsync();
        }

        public ObservableCollection<Domain> ListDomains
        {
            get => _listDomains ?? new Domain().DomainList;
            set
            {
                _listDomains = value;
                OnPropertyChanged(nameof(ListDomains));
            }
        }

        public string LoadingMessage
        {
            get => _loadingMessage;
            set
            {
                _loadingMessage = value;
                OnPropertyChanged(nameof(LoadingMessage));
            }
        }



        private void _initializeTypeList()
        {
            _typeList = new Dictionary<Type, IList>
            {
                { typeof(PlayerModel), CommonResources.Players = new ObservableCollection<PlayerModel>() },
                { typeof(DisplayUnitModel), CommonResources.DisplayUnits = new ObservableCollection<DisplayUnitModel>() },
                { typeof(FrameModel), CommonResources.Frames = new ObservableCollection<FrameModel>() },
                { typeof(DayPartModel), CommonResources.DayParts = new ObservableCollection<DayPartModel>() },
                { typeof(UserModel), CommonResources.Users = new ObservableCollection<UserModel>() },
                { typeof(ContainerModel), CommonResources.Containers = new ObservableCollection<ContainerModel>() },
                { typeof(GroupModel), CommonResources.Groups = new ObservableCollection<GroupModel>() },
                { typeof(ContainerScopeModel), CommonResources.Container_Scopes = new ObservableCollection<ContainerScopeModel>() },
                { typeof(ContainerScopeRelationModel), CommonResources.Container_Scope_Relations = new ObservableCollection<ContainerScopeRelationModel>() }
            };
        }

        private async Task _loadDataAsync()
        {
            foreach (var domain in ListDomains)
            {
                _currentDomain = domain;
                LoadingMessage += $"Loading resources for {domain.Name}:\n" +
                                  "--------------------------------------------------------------------------------------\n";
                _broadsignService.Token = domain.Token;

                await _processResourceAsync<PlayerModel>("/host/v17", "host");
                await _processResourceAsync<FrameModel>("/skin/v7", "skin");
                await _processResourceAsync<DisplayUnitModel>("/display_unit/v12", "display_unit");
                await _processResourceAsync<ContainerModel>("/container/v9", "container");
                await _processResourceAsync<ContainerScopeModel>("/container_scope/v1", "container_scope");
                await _processResourceAsync<ContainerScopeRelationModel>("/container_scope_relationship/v1", "container_scope_relationship");
                await _processResourceAsync<UserModel>("/user/v13", "user");
            }

            Mediator.Notify("HomeViewModel", "");
            Messenger.Default.Send(ListDomains, "HomeViewModel");

            File.WriteAllText(".\\text.txt", LoadingMessage);
        }

        private async Task _processResourceAsync<T>(string path, string resourceName) where T : IBroadsignAPIModel
        {
            if (!_typeList.TryGetValue(typeof(T), out var typeList))
                throw new InvalidOperationException($"No collection found for type {typeof(T)}");

            var resourceCollection = (ObservableCollection<T>)typeList;
            var resources = await _broadsignService.GetResources<T>(path, resourceName);

            foreach (var resource in resources)
            {
                if (resource.Active)
                {
                    resource.AssignedDomain = _currentDomain;
                    resourceCollection.Add(resource);
                }
            }

            LoadingMessage += $"\n{resourceName}: {resources.Count(x => x.Active)}\n";
        }

    }
}
