console.log('hello into product js.');

window.addEventListener('load', () => {
    let categoryFilters = document.querySelectorAll('#category-filters li');
    let reviewItems = document.querySelectorAll('.reviewCard');

    if (categoryFilters.length && reviewItems.length) {
        categoryFilters.forEach(function (filter) {
            filter.addEventListener('click', function (e) {
                e.preventDefault();

                // Remove active class from all filters
                categoryFilters.forEach(function (el) {
                    el.classList.remove('filter-active');
                });

                // Add active class to the clicked filter
                this.classList.add('filter-active');

                // Show/hide rewards items based on the filter
                reviewItems.forEach(function (item) {
                    let filterValue = filter.getAttribute('data-filter');
                    console.log(filter, 'filter');
                    console.log('itemclasslist', item.classList)
                    if (filterValue === '*') {
                        // Show all items when "All" filter is selected
                        item.style.display = '';
                    } else if (filterValue === '.5') {
                        // Show items with the class "filter-vouchers"
                        if (item.classList.contains('5')) {
                            item.style.display = '';
                        } else {
                            item.style.display = 'none';
                        }
                    } else if (filterValue === '.4') {
                        // Show items with the class "filter-products"
                        if (item.classList.contains('4')) {
                            item.style.display = '';
                        } else {
                            item.style.display = 'none';
                        }
                    } else if (filterValue === '.3') {
                        // Show items with the class "filter-limited"
                        if (item.classList.contains('3')) {
                            item.style.display = '';
                        } else {
                            item.style.display = 'none';
                        }
                    } else if (filterValue === '.2') {
                        // Show items with the class "filter-limited"
                        if (item.classList.contains('2')) {
                            item.style.display = '';
                        } else {
                            item.style.display = 'none';
                        }
                    } else if (filterValue === '.1') {
                        // Show items with the class "filter-limited"
                        if (item.classList.contains('1')) {
                            item.style.display = '';
                        } else {
                            item.style.display = 'none';
                        }
                    } else if (filterValue === '.0') {
                        // Show items with the class "filter-limited"
                        if (item.classList.contains('0')) {
                            item.style.display = '';
                        } else {
                            item.style.display = 'none';
                        }
                    } else {
                        // Show items with the selected filter class
                        if (item.classList.contains(filterValue)) {
                            item.style.display = '';
                        } else {
                            item.style.display = 'none';
                        }
                    }
                })
            });
        });
    }
});
