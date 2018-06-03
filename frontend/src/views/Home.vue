<template>
  <div class="home">
    <b-container>
      <div id="controller">
        <b-button variant="success" id="findMeButton" v-if="canGeo && pharmacyData !== undefined" @click="geolocate()"><i class="fas fa-location-arrow"></i> Find Near Me</b-button>
        <b-button disabled variant="secondary" v-else><i class="fas fa-location-arrow"></i> Not available</b-button>
        <p style="float: right;" v-if="userLat !== 0.00 && userLong !== 0.00">Latitude: {{userLat}},  Longitude: {{userLong}}</p>
      </div>
      <hr>
      <div id="mapContainer">
        <GmapMap
          :center="{lat: mapLat, lng: mapLong}"
          :zoom="userZoom"
          :options="{scrollwheel: false}"
          id="mapObject">
          <GmapMarker
            :key="pharmacy.id"
            :id="'pharmaMarker' + pharmacy.id"
            v-for="pharmacy in pharmacyData"
            :position="{lat: pharmacy.latitude, lng: pharmacy.longitude}"
            :title="pharmacy.name">
          </GmapMarker>
        </GmapMap>
        <div id="pharmacyList">
          <div v-if="pharmacyData === undefined">
            <div class="text-center">
              <h4>Loading...</h4>
              <img src="@/assets/spinner.gif" />
            </div>
          </div>
          <div v-else>
            <pharmacy-list ref="pharmacyList" :pharmacyData="pharmacyData" selectedPharmacy="0" v-on:selectedPharmacy="emitSelectedId" />
          </div>
        </div>
      </div>
    </b-container>
  </div>
</template>

<script>
import Globals from '../globals'
import axios from 'axios'

import PharmacyList from '@/components/PharmacyList.vue'

export default {
  name: 'home',
  data() {
    return {
      pharmacyData: undefined,
      selectedPharmacyId: 0,
      gmapsKey: Globals.getGoogleKey(),
      canGeo: false,
      userLat: 0.00,
      userLong: 0.00,
      userZoom: 8,
      mapLat: 39.0997,
      mapLong: -94.5786
    }
  },
  components: {
    "pharmacy-list": PharmacyList
  },
  created() {
    axios({
      method: 'get',
      url: Globals.getBackendUrl() + "/pharmacies",
    }).then((resolvedResponse) => {
      this.pharmacyData = resolvedResponse.data;
    }, (rejectedResponse) => {
      this.pharmacyData = undefined;
      console.log("REJECTED!", rejectedResponse);
    })
  },
  mounted() {
    // First, check to see if the geolocation feature is available in the browser.
    if ("geolocation" in navigator) {
      /* geolocation is available */
      this.canGeo = true;
    } else {
      /* geolocation IS NOT available */
      this.canGeo = false;
    }

    console.log(this.canGeo ? "Detected geolocation features." : "Could not detect geolocation features.");
  },
  methods: {
    emitSelectedId(id) {
      this.selectedPharmacyId = id;
    },
    buildInfoWindowHtml(pharmacy) {
      return "<h1>" + pharmacy.name + "</h1>\n" + "<p>" + pharmacy.address + "</p>\n" + "<p>" + pharmacy.city + ", " + pharmacy.state + ", " + pharmacy.zip + "</p>\n";
    },
    toggleInfoWindow(pharmacyId) {

    },
    geolocate() {
      if (this.canGeo) {
        var userPosLat = 0.0;
        var userPosLong = 0.0;

        var geoPromptCheckInterval = undefined;

        console.log("Fetching user's location...");

        // Check geolocation permission every 250ms.
        if ("permissions" in navigator) {
          geoPromptCheckInterval = window.setInterval(function() {
            // Get user prompt status.
            navigator.permissions.query({name:'geolocation'}).then(function(result) {
              if (result.state === 'granted') {
                console.log("Got permission for geolocation! :^)");
                window.clearInterval(geoPromptCheckInterval);
              } else if (result.state == 'prompt') {
                console.log("Waiting for user prompt for geolocation.");
              }
              // Don't do anything if the permission was denied.
            });
          }, 250);
        }

        var otherThis = this;

        navigator.geolocation.getCurrentPosition(function(position) {
          console.log("Lat: " + position.coords.latitude + "\nLong: " + position.coords.longitude);
          otherThis.userLat = position.coords.latitude;
          otherThis.userLong = position.coords.longitude;
          otherThis.userZoom = 15;
          window.clearInterval(geoPromptCheckInterval); // Fix a weird Firefox issue.

          // Now we grab the closest pharmacy to the user.
          axios({
            method: 'get',
            url: Globals.getBackendUrl() + "/pharmacies/fetchNearest/" + otherThis.userLat + "/" + otherThis.userLong
          }).then((resolvedResponse) => {
            var pharmacyData = resolvedResponse.data.key;
            var pharmacyDistance = resolvedResponse.data.value;

            otherThis.mapLat = pharmacyData.latitude;
            otherThis.mapLong = pharmacyData.longitude;

            otherThis.$refs.pharmacyList.currentPharmacyId = pharmacyData.id;

            // Now we grab all of the nearest pharmacies to reconstruct the pharmacy list.
            axios({
              method: 'get',
              url: Globals.getBackendUrl() + "/pharmacies/fetchAllNearest/" + otherThis.userLat + "/" + otherThis.userLong
            }).then((resolved) => {
              var transientData = [];
              resolved.data.forEach(pharmacyWithDistance => {
                var obj = pharmacyWithDistance.key;
                obj.distance = pharmacyWithDistance.value;

                transientData.push(obj);
              });

              otherThis.pharmacyData = transientData;
            }, (rejected) => {

            });

            
          }, (rejectedResponse) => {
            console.log("REJECTED", rejectedResponse);
          });
        });
      } else {
        console.warn("Can't use geolocation!");
      }
    }
  }
}
</script>

<style>
.home {
  padding-top: 25px;
  padding-bottom: 25px;
}

#mapObject {
  width: 75%;
  min-height: 500px;
  height: 100%;
  float: left;
}

#pharmacyList {
  float: right;
  display: block;
  width: 25%;
  min-height: 500px;
  max-height: 500px;
  overflow-y: scroll;
}

/* Smartphone and smaller */
@media only screen and (max-width: 37.5em) {
  #mapObject {
    float: unset;
    width: 100%;
  }

  #pharmacyList {
    float: unset;
    padding-top: 15px;
    width: 100%;
    overflow-y: initial;
  }
}
</style>
