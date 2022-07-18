"use strict";

class HestiaGoogleMapHelper {
    static #defaultMapCenterLat = 0;
    static #defaultMapCenterLng = 0;
    static get DEFAULT_MAP_CENTER() {
        return HestiaGoogleMapHelper.createLatLng(
            HestiaGoogleMapHelper.#defaultMapCenterLat,
            HestiaGoogleMapHelper.#defaultMapCenterLng);
    }
    static setDefaultMapCenter(lat, lng) {
        HestiaGoogleMapHelper.#defaultMapCenterLat = lat;
        HestiaGoogleMapHelper.#defaultMapCenterLng = lng;
    }

    static #defaultMapZoom = 16;
    static get DEFAULT_MAP_ZOOM() { return HestiaGoogleMapHelper.#defaultMapZoom; }
    static setDefaultMapZoom(zoom) {
        HestiaGoogleMapHelper.#defaultMapZoom = zoom;
    }

    // TODO: duplicate initialize check & type check
    static #initializers = [];
    static #callback = undefined;
    static addInitializer(initializer) {
        HestiaGoogleMapHelper.#initializers.push(initializer);
    }
    static initializeMap(key, callback) {
        this.#callback = callback;
        window.__hestiaGoogleMapHelperInitializeCallback__ = function () {
            HestiaGoogleMapHelper.initialize();
            //HestiaGoogleMapHelper._callback();
        };
        const script = document.createElement("script");
        script.async = true;
        script.defer = true;
        script.src = `https://maps.googleapis.com/maps/api/js?v=quarterly&callback=__hestiaGoogleMapHelperInitializeCallback__&key=${key}`;
        document.body.appendChild(script);
    }
    static initialize() {
        HestiaGoogleMapHelper.PopUp = class extends google.maps.OverlayView {
            static Direction = class {
                static get TOP() { return "top"; }

                static get BOTTOM() { return "bottom"; }

                static get LEFT() { return "left"; }

                static get RIGHT() { return "right"; }
            }

            constructor(map, position, content, direction = HestiaGoogleMapHelper.PopUp.Direction.TOP) {
                super();
                this.#map = map;
                this.#position = position;
                content.classList.add("popup-bubble");
                this.#containerDiv = document.createElement("div");
                this.#containerDiv.classList.add("popup-container");
                this.#containerDiv.appendChild(content);
                HestiaGoogleMapHelper.PopUp.preventMapHitsAndGesturesFrom(this.#containerDiv);
                this.#direction = direction;
                this.setMap(map);
            }

            #map = undefined;
            #position = undefined;
            #containerDiv = undefined;
            #direction = undefined;

            /** Called when the popup is added to the map. */
            onAdd() {
                this.getPanes().floatPane.appendChild(this.#containerDiv);
            }

            /** Called when the popup is removed from the map. */
            onRemove() {
                if (this.#containerDiv.parentElement) {
                    this.#containerDiv.parentElement.removeChild(this.#containerDiv);
                }
            }

            /** Called each frame when the popup needs to draw itself. */
            draw() {
                const projection = this.getProjection();
                if (Common.isNullOrUndefined(projection)) {
                    return;
                }
                const divPosition = projection.fromLatLngToDivPixel(this.#position);
                let xOffset = 0;
                let yOffset = 0;
                switch (this.#direction) {
                    case HestiaGoogleMapHelper.PopUp.Direction.BOTTOM:
                        yOffset = 40;
                        break;
                    case HestiaGoogleMapHelper.PopUp.Direction.LEFT:
                        xOffset = -70;
                        break;
                    case HestiaGoogleMapHelper.PopUp.Direction.RIGHT:
                        xOffset = 70;
                        break;
                    case HestiaGoogleMapHelper.PopUp.Direction.TOP:
                    default:
                        yOffset = -40;
                }
                this.#containerDiv.style.left = (divPosition.x + xOffset) + "px";
                this.#containerDiv.style.top = (divPosition.y + yOffset) + "px";
            }

            setPosition(position) {
                this.#position = position;
                this.draw();
            }

            hide() {
                this.setMap(null);
            }

            show() {
                this.setMap(this.#map);
            }
        };
        const popupStyle = document.createElement("style");
        popupStyle.textContent = `
            .popup-bubble {
                position: absolute;
                top: 0;
                left: 0;
                transform: translate(-50%, -100%);
                background-color: white;
                color: black;
                text-align: center;
                font-size: 1.5em;
                padding: 5px;
                border-radius: 5px;
                overflow-y: auto;
                box-shadow: 0px 2px 10px 1px rgba(0, 0, 0, 0.5);
            }
            .popup-container {
                cursor: auto;
                height: 0;
                position: absolute;
                width: 200px;
            }`;
        document.head.appendChild(popupStyle);

        HestiaGoogleMapHelper.#initializers.forEach(x => x());

        HestiaGoogleMapHelper.#callback();
    }

    static createTrafficLayer(map) {
        return new HestiaGoogleMapHelper.#TrafficLayerWrapper(map);
    }

    static #TrafficLayerWrapper = class {
        constructor(map) {
            this.#layer = new google.maps.TrafficLayer();
            this.#layer.setMap(null);
            this.#map = map;
        }

        #layer = undefined;
        #map = undefined;

        toggle() {
            if (this.#layer.getMap() === null) {
                this.enable();
            } else {
                this.disable();
            }
        }

        enable() {
            this.#layer.setMap(this.#map);
        }

        disable() {
            this.#layer.setMap(null);
        }
    }

    static createLatLng(lat, lng) {
        return { lat: lat, lng: lng };
    }

    static PopUp = undefined;

    static MarkerBuilder = class {
        constructor(map, iconSize = null) {
            this.#config.map = map;
            this.#config.size = iconSize ?? new google.maps.Size(50, 50);
            google.maps.Marker.prototype.getElement = google.maps.Marker.prototype.getElement || function () {
                return document.querySelector(`img[src="${this.getIcon().url}"]`);
            };
        }

        #config = {
            map: undefined,
            size: undefined,
            legend: undefined,
        };

        setMarker(info, data = undefined) {
            const marker = new google.maps.Marker({
                position: HestiaGoogleMapHelper.createLatLng(info.lat, info.lng),
                map: this.#config.map,
                icon: {
                    url: this.#getIconUrlFromInfo(info),
                    scaledSize: this.#config.size,
                },
                data: data,
            });
            return marker;
        }

        updateMarkerIcon(info) {
            info.marker.setIcon({
                url: this.#getIconUrlFromInfo(info),
                scaledSize: this.#config.size,
            });
        }

        #getIconUrlFromInfo(info) {
            return `${info.iconUrl}?id=${info.id}`;
        }
    }

    static toggleSatellite(map) {
        let newMapId;
        switch (map.getMapTypeId()) {
            case "hybrid":
                newMapId = "roadmap";
                break;
            case "roadmap":
            default:
                newMapId = "hybrid";
        }
        map.setMapTypeId(newMapId);
    }
}
