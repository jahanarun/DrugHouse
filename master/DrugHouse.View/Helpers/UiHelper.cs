using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace DrugHouse.View.Helpers
{
    internal class UiHelper
    {
        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
            where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
    }


    public static class DataGridExtensions
    {
        //public static DataGridCell GetCell(this DataGrid grid, DataGridRow row, int columnIndex = 0)
        //{
        //    if (row == null) return null;

        //    var presenter = row.FindVisualChild<DataGridCellsPresenter>();
        //    if (presenter == null) return null;

        //    var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
        //    if (cell != null) return cell;

        //    // now try to bring into view and retreive the cell
        //    grid.ScrollIntoView(row, grid.Columns[columnIndex]);
        //    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);

        //    return cell;
        //}


        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {                                
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {                            
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)                           
                    return (T)child;                                       
                else                                                       
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)                  
                        return childOfChild;                   
                }                                              
            }                                                  
            return null;                                       
        }


        public static DataGridCell GetCell(DataGrid dataGrid, DataGridRow rowContainer, int column)
        {

            if (rowContainer != null)
            {

                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);

                if (presenter == null)
                {

                    /* if the row has been virtualized away, call its ApplyTemplate() method 

                     * to build its visual tree in order for the DataGridCellsPresenter

                     * and the DataGridCells to be created */

                    rowContainer.ApplyTemplate();

                    presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);

                }

                if (presenter != null)
                {

                    DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;

                    if (cell == null)
                    {

                        /* bring the column into view

                         * in case it has been virtualized away */

                        dataGrid.ScrollIntoView(rowContainer, dataGrid.Columns[column]);

                        cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;

                    }

                    return cell;

                }

            }

            return null;

        }


    }
}