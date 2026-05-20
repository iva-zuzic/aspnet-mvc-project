$(function () {
    $('.datetime-picker-input').each(function () {
        const $displayInput = $(this);
        const hiddenId = $displayInput.data('hidden-id');
        const $hiddenInput = $('#' + hiddenId);

        const $wrapper = $displayInput.closest('.datetime-picker');
        const $hint = $wrapper.find('.datetime-format-hint');
        const $error = $wrapper.find('.datetime-error');

        const language = navigator.language ? navigator.language.toLowerCase() : 'hr';
        const isCroatian = language.startsWith('hr');

        $hint.text(isCroatian
            ? 'Format: dd.MM.yyyy. HH:mm'
            : 'Format: MM/dd/yyyy HH:mm');

        function pad(value) {
            return value.toString().padStart(2, '0');
        }

        function toIsoLocal(date) {
            return date.getFullYear() + '-' +
                pad(date.getMonth() + 1) + '-' +
                pad(date.getDate()) + 'T' +
                pad(date.getHours()) + ':' +
                pad(date.getMinutes());
        }

        function formatForDisplay(date) {
            const day = pad(date.getDate());
            const month = pad(date.getMonth() + 1);
            const year = date.getFullYear();
            const hours = pad(date.getHours());
            const minutes = pad(date.getMinutes());

            if (isCroatian) {
                return day + '.' + month + '.' + year + '. ' + hours + ':' + minutes;
            }

            return month + '/' + day + '/' + year + ' ' + hours + ':' + minutes;
        }

        function parseIsoLocal(value) {
            if (!value) {
                return null;
            }

            const match = value.match(/^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2})$/);

            if (!match) {
                return null;
            }

            return createDate(
                Number(match[1]),
                Number(match[2]),
                Number(match[3]),
                Number(match[4]),
                Number(match[5])
            );
        }

        function createDate(year, month, day, hours, minutes) {
            const date = new Date(year, month - 1, day, hours, minutes);

            const isValid =
                date.getFullYear() === year &&
                date.getMonth() === month - 1 &&
                date.getDate() === day &&
                date.getHours() === hours &&
                date.getMinutes() === minutes;

            return isValid ? date : null;
        }

        function parseDisplayValue(value) {
            const text = value.trim();

            if (text.length === 0) {
                return null;
            }

            const hrMatch = text.match(/^(\d{1,2})\.(\d{1,2})\.(\d{4})\.?\s+(\d{1,2}):(\d{2})$/);

            if (hrMatch) {
                return createDate(
                    Number(hrMatch[3]),
                    Number(hrMatch[2]),
                    Number(hrMatch[1]),
                    Number(hrMatch[4]),
                    Number(hrMatch[5])
                );
            }

            const enMatch = text.match(/^(\d{1,2})\/(\d{1,2})\/(\d{4})\s+(\d{1,2}):(\d{2})$/);

            if (enMatch) {
                return createDate(
                    Number(enMatch[3]),
                    Number(enMatch[1]),
                    Number(enMatch[2]),
                    Number(enMatch[4]),
                    Number(enMatch[5])
                );
            }

            return null;
        }

        function showError(message) {
            $displayInput.addClass('input-validation-error');
            $error.text(message);
        }

        function clearError() {
            $displayInput.removeClass('input-validation-error');
            $error.text('');
        }

        function setDate(date) {
            $hiddenInput.val(toIsoLocal(date));
            $displayInput.val(formatForDisplay(date));
            clearError();
        }

        function validateAndSync() {
            const value = $displayInput.val().trim();

            if (value.length === 0) {
                $hiddenInput.val('');
                showError('Datum i vrijeme isteka su obavezni.');
                return;
            }

            const parsedDate = parseDisplayValue(value);

            if (parsedDate === null) {
                $hiddenInput.val('');
                showError('Unesite datum i vrijeme u ispravnom formatu.');
                return;
            }

            if (parsedDate <= new Date()) {
                $hiddenInput.val('');
                showError('Datum isteka mora biti u budućnosti.');
                return;
            }

            setDate(parsedDate);
        }

        const initialDate = parseIsoLocal($hiddenInput.val());

        if (initialDate !== null) {
            $displayInput.val(formatForDisplay(initialDate));
        }

        $displayInput.on('input', function () {
            $hiddenInput.val('');
            clearError();
        });

        $displayInput.on('blur', function () {
            validateAndSync();
        });
    });
});