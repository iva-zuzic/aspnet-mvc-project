$(function () {
    const $input = $('#globalSearchInput');
    const $results = $('#globalSearchResults');

    let timer = null;
    let activeRequest = null;

    function sakrijRezultate() {
        $results.hide().empty();
    }

    function prikaziPrazno() {
        $results
            .empty()
            .append('<div class="nav-search-empty">Nema rezultata.</div>')
            .show();
    }

    function dodajRezultat(item) {
        const $link = $('<a class="nav-search-item"></a>');

        $link.attr('href', item.url);

        const $type = $('<span class="nav-search-type"></span>').text(item.tip);
        const $title = $('<span class="nav-search-title"></span>').text(item.naziv);
        const $subtitle = $('<span class="nav-search-subtitle"></span>').text(item.opis);

        $link.append($type);
        $link.append($title);
        $link.append($subtitle);

        $results.append($link);
    }

    $input.on('input', function () {
        const term = $(this).val().trim();

        clearTimeout(timer);

        if (activeRequest) {
            activeRequest.abort();
        }

        if (term.length < 1) {
            sakrijRezultate();
            return;
        }

        timer = setTimeout(function () {
            activeRequest = $.ajax({
                url: '/api/global-search',
                type: 'GET',
                data: { term: term },
                dataType: 'json',
                success: function (data) {
                    $results.empty();

                    if (data.length === 0) {
                        prikaziPrazno();
                        return;
                    }

                    $.each(data, function (i, item) {
                        dodajRezultat(item);
                    });

                    $results.fadeIn(150);
                },
                error: function (xhr, status) {
                    if (status === 'abort') {
                        return;
                    }

                    $results
                        .empty()
                        .append('<div class="nav-search-empty">Greška kod pretrage.</div>')
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