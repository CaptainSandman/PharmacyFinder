<template>
  <div class="home">
    <b-container>
      <div id="controller">
        <b-button variant="success" id="findMeButton" v-if="canGeo" @click="geolocate()"><i class="fas fa-location-arrow"></i> Find Near Me</b-button>
        <b-button disabled variant="secondary" v-else><i class="fas fa-location-arrow"></i> Not available</b-button>
      </div>
      <hr>
      <div id="mapContainer">
        <GmapMap
          :center="{lat:38.8814, lng: -94.8191}"
          :zoom="9"
          :options="{scrollwheel: false}"
          id="mapObject">
        </GmapMap>
        <div id="pharmacyList">
          <pharmacy-list :pharmacyData="this.pharmacyData" />
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
      pharmacyData: [],
      gmapsKey: Globals.getGoogleKey(),
      canGeo: false
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

        navigator.geolocation.getCurrentPosition(function(position) {
          console.log("Lat: " + position.coords.latitude + "\nLong: " + position.coords.longitude);
          userPosLat = position.coords.latitude;
          userPosLong = position.coords.longitude;
          window.clearInterval(geoPromptCheckInterval); // Fix a weird Firefox issue.
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
