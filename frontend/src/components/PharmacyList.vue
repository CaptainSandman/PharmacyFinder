<template>
  <b-container>
    <b-list-group class="list">
      <b-list-group-item :key="pharmacy.id" v-for="pharmacy in pharmacyData" @click="setPharmacyId(pharmacy.id)" :active="currentPharmacyId === pharmacy.id">
        {{pharmacy.name}}
        <br/>
        <small>{{pharmacy.city}}, {{pharmacy.state}}</small>
      </b-list-group-item>
    </b-list-group>
  </b-container>
</template>

<script>
export default {
  name: 'PharmacyList',
  props: [
    'pharmacyData'
  ],
  data() {
    return {
      currentPharmacyId: 0
    }
  },
  methods: {
    setPharmacyId(id) {
      this.currentPharmacyId = id
    }
  }
}
</script>

<style>

</style>
