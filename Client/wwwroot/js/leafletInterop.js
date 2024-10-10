window.leafletInterop = {
    createMap: function (elementId, lat, lng, zoom) {
        var map = L.map(elementId).setView([lat, lng], zoom);
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '© OpenStreetMap contributors'
        }).addTo(map);

        map.on('contextmenu', function (e) {
            // Create a popup with an "Add Location" button
            var popup = L.popup()
                .setLatLng(e.latlng)
                .setContent('<button onclick="leafletInterop.showAddLocationModal(' + e.latlng.lat + ',' + e.latlng.lng + '); leafletInterop.closePopup();">Add Location</button>')
                .openOn(map);

            // Store the popup to close it later
            leafletInterop.currentPopup = popup;
        });

        return map;
    },
    showAddLocationModal: function (lat, lng) {
        // Call the Blazor method to show the modal
        DotNet.invokeMethodAsync('LocationFinder.Client', 'ShowAddLocationModal', lat, lng);
    },
    closePopup: function () {
        // Close the current popup
        if (leafletInterop.currentPopup) {
            leafletInterop.currentPopup._map.closePopup(leafletInterop.currentPopup);
            leafletInterop.currentPopup = null; // Clear the stored popup reference
        }
    },
    addMarkers: function (map, markers) {
        markers.forEach(marker => {
            var markerOptions = {};
            if (marker.iconUrl) {
                var customIcon = L.icon({
                    iconUrl: marker.iconUrl,
                    iconSize: [32, 32], // Size of the icon
                    iconAnchor: [16, 32], // Point of the icon that corresponds to marker's location
                    popupAnchor: [0, -32] // Position of the popup relative to the icon
                });
                markerOptions.icon = customIcon;
            }

            var leafletMarker = L.marker([marker.lat, marker.lng], markerOptions).addTo(map);
            if (marker.popupContent) {
                leafletMarker.bindPopup(marker.popupContent, {
                    closeButton: true,
                    className: 'custom-popup'
                });
            }
        });
    }
};
