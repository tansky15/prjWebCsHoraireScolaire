
    $(function () {
        // Initialisez le date picker pour les champs avec la classe 'datepicker'
        $(".datepicker").datepicker({
            dateFormat: "yy-mm-dd", // Format de la date (année-mois-jour)
            changeMonth: true, // Permet de changer le mois
            changeYear: true, // Permet de changer l'année
            yearRange: "1900:2099" // Plage d'années disponibles
        });
    });