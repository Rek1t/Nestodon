using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Controls.ListViewExtended
{
    /// <summary>
    /// ListView étendue avec fonctions de recherche et SemanticZoom automatique
    /// </summary>
    public sealed class ListViewExtended : Control
    {
        private readonly ObservableCollection<object> _searchResults = new ObservableCollection<object>();
        private bool _setItemsSource = false;

        #region template part name

        private const string ListItemsName = "ListItems";
        private const string SemanticZoomName = "SemanticZoom";
        private const string ListChampsRechercheName = "ListChampsRecherche";
        private const string SuggestBoxName = "SuggestBox";

        #endregion

        #region template part fields

        private ListView _listItems;
        private SemanticZoom _semanticZoom;
        private ComboBox _listChampsRecherche;
        private AutoSuggestBox _suggestBox;

        #endregion

        #region dependency properties

        /// <summary>
        /// BackgroundFilter Dependency Property
        /// </summary>
        public static readonly DependencyProperty FilterMemberPathProperty =
            DependencyProperty.Register(
                "FilterMemberPath",
                typeof(string),
                typeof(ListViewExtended),
                new PropertyMetadata(null));

        public string FilterMemberPath
        {
            get { return (string)GetValue(FilterMemberPathProperty); }
            set { SetValue(FilterMemberPathProperty, value); }
        }

        /// <summary>
        /// ItemsSource Dependency Property
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(object),
                typeof(ListViewExtended),
                new PropertyMetadata(null));

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// ItemTemplate Dependency Property
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(
                "ItemTemplate",
                typeof(DataTemplate),
                typeof(ListViewExtended),
                new PropertyMetadata(null));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>
        /// SelectionMode Dependency Property
        /// </summary>
        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register(
                "SelectionMode",
                typeof(SelectionMode),
                typeof(ListViewExtended),
                new PropertyMetadata(Windows.UI.Xaml.Controls.SelectionMode.Single));

        public SelectionMode SelectionMode
        {
            get { return (SelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        /// <summary>
        /// SelectedItem Dependency Property
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
                "SelectedItem",
                typeof(object),
                typeof(ListViewExtended),
                new PropertyMetadata(null));

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// PlaceHolderSearch Dependency Property
        /// </summary>
        public static readonly DependencyProperty PlaceHolderSearchProperty =
            DependencyProperty.Register(
                "PlaceHolderSearch",
                typeof(string),
                typeof(ListViewExtended),
                new PropertyMetadata(null));

        public string PlaceHolderSearch
        {
            get { return (string)GetValue(PlaceHolderSearchProperty); }
            set { SetValue(PlaceHolderSearchProperty, value); }
        }

        #endregion

        public ListViewExtended()
        {
            this.DefaultStyleKey = typeof(ListViewExtended);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _listItems = GetTemplateChild(ListItemsName) as ListView;
            if (_listItems != null)
            {
                _listItems.SelectionChanged += ListItemsSelectionChanged;
            }

            _semanticZoom = GetTemplateChild(SemanticZoomName) as SemanticZoom;
            _listChampsRecherche = GetTemplateChild(ListChampsRechercheName) as ComboBox;
            _suggestBox = GetTemplateChild(SuggestBoxName) as AutoSuggestBox;

            //Construction de la liste des champs dispos pour la recherche
            CreateItemsListChampsRecherche();

            if (_suggestBox != null && _listChampsRecherche != null)
            {
                if (_listChampsRecherche.ItemsSource == null)
                {
                    _suggestBox.IsEnabled = false;
                    _listChampsRecherche.IsEnabled = false;
                }
                _suggestBox.TextChanged += SuggestBoxTextChanged;
                _suggestBox.SuggestionChosen += SuggestBoxSuggestionChosen;
                _suggestBox.QuerySubmitted += SuggestBoxQuerySubmitted;
                _suggestBox.ItemsSource = _searchResults;
            }

            //Construction des items groupés pour la liste
            CreateItemsSource();
        }

        #region events

        private void ListItemsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_setItemsSource)
            {
                foreach (var item in e.AddedItems)
                {
                    this.SelectedItem = item;
                }
            }
        }

        private void SuggestBoxQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (_listChampsRecherche.SelectedItem != null)
            {
                var searchValue = args.ChosenSuggestion != null
                    ? args.ChosenSuggestion.GetType()
                        .GetProperty(_listChampsRecherche.SelectedItem.ToString())
                        .GetValue(args.ChosenSuggestion)
                        .ToString()
                        .ToLower()
                    : args.QueryText;

                var results =
                    ToList()?
                        .Where(
                            o =>
                                o.GetType()
                                    .GetProperty(_listChampsRecherche.SelectedItem.ToString())
                                    .GetValue(o)
                                    .ToString().ToLower()
                                    .Contains(searchValue));

                if (results != null && results.Any())
                {
                    SetItemsSourceToListView(results.ToList());
                }
            }
        }

        private void SuggestBoxSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (_listChampsRecherche.SelectedItem != null)
            {
                var searchValue =
                    args.SelectedItem.GetType()
                        .GetProperty(_listChampsRecherche.SelectedItem.ToString())
                        .GetValue(args.SelectedItem)
                        .ToString()
                        .ToLower();

                var results =
                    ToList()?
                        .Where(
                            o =>
                                o.GetType()
                                    .GetProperty(_listChampsRecherche.SelectedItem.ToString())
                                    .GetValue(o)
                                    .ToString().ToLower()
                                    .Contains(searchValue));

                if (results != null && results.Any())
                {
                    SetItemsSourceToListView(results.ToList());
                }
            }
        }

        private void SuggestBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            _searchResults.Clear();
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput && _listChampsRecherche.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(sender.Text))
                {
                    var results =
                        ToList()?
                            .Where(
                                o =>
                                    o.GetType()
                                        .GetProperty(_listChampsRecherche.SelectedItem.ToString())
                                        .GetValue(o)
                                        .ToString().ToLower()
                                        .Contains(sender.Text.ToLower()));
                    if (results != null)
                    {
                        foreach (var result in results)
                        {
                            _searchResults.Add(result);
                        }
                    }
                }
                else
                {
                    CreateItemsSource();
                }
            }
        }

        #endregion

        #region private

        /// <summary>
        /// Permet de créer la source pour la liste et d'en faire une liste avec SemanticZoom
        /// </summary>
        private void CreateItemsSource()
        {
            var items = ToList();
            SetItemsSourceToListView(items);
        }

        /// <summary>
        /// Affecte les résultats à la liste
        /// </summary>
        private void SetItemsSourceToListView(List<object> items)
        {
            if (items != null && items.Any())
            {
                var itemsGrouped = new ObservableCollection<ItemsGroup>();
                var itemsOrdered = items.GroupBy(o => o.GetType().GetProperty(FilterMemberPath).GetValue(o).ToString().Substring(0, 1));
                foreach (var @group in itemsOrdered.OrderBy(o => o.Key))
                {
                    itemsGrouped.Add(new ItemsGroup(group));
                }

                var collection = new CollectionViewSource
                {
                    IsSourceGrouped = true,
                    ItemsPath = new PropertyPath("Items"),
                    Source = itemsGrouped
                };

                _setItemsSource = true;
                _listItems.ItemsSource = collection.View;
                _listItems.SelectedIndex = -1;
                _setItemsSource = false;

                ((ListViewBase)_semanticZoom.ZoomedOutView).ItemsSource = collection.View.CollectionGroups;
                
            }
        }

        /// <summary>
        /// Transform l'ItemsSource en List&lt;object&gt;
        /// </summary>
        /// <returns></returns>
        private List<object> ToList()
        {
            var items = ItemsSource as IEnumerable;
            return items?.Cast<object>()?.ToList();
        }

        /// <summary>
        /// Permet de trouver les champs du type de ItemsSource pour proposer une liste sur les champs de recherche
        /// </summary>
        private void CreateItemsListChampsRecherche()
        {
            //On prend le premier élément de la liste et on cherche les propriétés notées comme étant prévues pour la recherche
            var items = ToList();
            var item = items.Any() ? items.First() : null;
            if (item != null)
            {
                var properties = ListViewExtendedSearchAttribute.GetPropertyInfosForSearch(item);
                if (properties != null)
                {
                    _listChampsRecherche.ItemsSource = properties;
                    _listChampsRecherche.SelectedIndex = 0;
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Structure pour les items de la liste
    /// </summary>
    public class ItemsGroup
    {
        public string Key { get; set; }
        public ObservableCollection<object> Items { get; set; }

        public ItemsGroup(IGrouping<object, object> group)
        {
            Key = group.Key.ToString().ToUpper();
            Items = new ObservableCollection<object>();
            foreach (var item in group)
            {
                Items.Add(item);
            }
        }
    }
}
