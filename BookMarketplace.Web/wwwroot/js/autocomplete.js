$(function () {
    $('.autocomplete-input').each(function () {
        var $input = $(this);

        if ($input.data('autocomplete-initialized')) {
            return;
        }

        $input.data('autocomplete-initialized', true);

        var apiUrl = $input.data('api-url');
        var uniqueId = $input.attr('id').replace('input-', '');

        var $hidden = $('#hidden-' + uniqueId);
        var $results = $('#results-' + uniqueId);

        var timer = null;

        $input.on('input', function () {
            var term = $(this).val().trim();

            $hidden.val('');

            clearTimeout(timer);

            if (term.length < 1) {
                $results.hide().empty();
                return;
            }

            timer = setTimeout(function () {
                $.ajax({
                    url: apiUrl,
                    type: 'GET',
                    data: { term: term },
                    dataType: 'json',
                    success: function (data) {
                        $results.empty();

                        if (data.length === 0) {
                            $results.append(
                                '<div class="autocomplete-item autocomplete-empty">Nema rezultata</div>'
                            );
                        } else {
                            $.each(data, function (i, item) {
                                var $item = $('<div class="autocomplete-item"></div>');

                                $item.text(item.naziv);

                                $item.on('mousedown', function () {
                                    $input.val(item.naziv);
                                    $hidden.val(item.id).trigger('change');
                                    $results.hide().empty();
                                });

                                $results.append($item);
                            });
                        }

                        $results.show();
                    },
                    error: function () {
                        $results
                            .empty()
                            .append('<div class="autocomplete-item autocomplete-empty">Greška kod dohvaćanja rezultata.</div>')
                            .show();
                    }
                });
            }, 250);
        });

        $input.on('blur', function () {
            setTimeout(function () {
                $results.hide();
            }, 200);
        });

        $input.on('focus', function () {
            if ($results.children().length > 0) {
                $results.show();
            }
        });
    });
});