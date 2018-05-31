<template>
  <div class="home">
    <b-container>
      <GmapMap
        :center="{lat:38.8814, lng: -94.8191}"
        :zoom="9"
        style="width: 640px; height: 480px">
      </GmapMap>
    </b-container>
  </div>
</template>

<script>
import Globals from '../globals'
import axios from 'axios'

export default {
  name: 'home',
  data() {
    return {
      gmapsKey: Globals.getGoogleKey()
    }
  },
  components: {
  },
  mounted() {

  }
}
</script>

<style>
.home {
  padding-top: 25px;
}

.map-responsive {
  overflow: hidden;
  padding-bottom: 56.25%;
  padding-right: 32.50%;
  position: relative;
  height: 0;
}

.map-responsive iframe {
  left: 0;
  top: 0;
  height: 100%;
  width: 100%;
  position: absolute;
}
</style>
