window.addEventListener('load', () => {
    let categoryFilters = document.querySelectorAll('#category-filters li');
    let rewardsItems = document.querySelectorAll('.rewards-item-card');

    if (categoryFilters.length && rewardsItems.length) {
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
                rewardsItems.forEach(function (item) {
                    let filterValue = filter.getAttribute('data-filter');
                    if (filterValue === '*') {
                        // Show all items when "All" filter is selected
                        item.style.display = 'block';
                    } else if (filterValue === '.filter-vouchers') {
                        // Show items with the class "filter-vouchers"
                        if (item.classList.contains('filter-vouchers')) {
                            item.style.display = 'block';
                        } else {
                            item.style.display = 'none';
                        }
                    } else if (filterValue === '.filter-products') {
                        // Show items with the class "filter-products"
                        if (item.classList.contains('filter-products')) {
                            item.style.display = 'block';
                        } else {
                            item.style.display = 'none';
                        }
                    } else if (filterValue === '.filter-limited') {
                        // Show items with the class "filter-limited"
                        if (item.classList.contains('filter-limited')) {
                            item.style.display = 'block';
                        } else {
                            item.style.display = 'none';
                        }
                    } else {
                        // Show items with the selected filter class
                        if (item.classList.contains(filterValue)) {
                            item.style.display = 'block';
                        } else {
                            item.style.display = 'none';
                        }
                    }
                })
            });
        });
    }
});
