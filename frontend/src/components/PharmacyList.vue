<template>
  <b-container>
    <b-list-group class="list">
      <pharmacy-list-item :key="pharmacy.id" v-for="pharmacy in pharmacyData" :pharmacy="pharmacy" @click.native="setPharmacyId(pharmacy.id)" :active="currentPharmacyId === pharmacy.id">
      </pharmacy-list-item>
    </b-list-group>
  </b-container>
</template>

<script>
import PharmacyListItem from './PharmacyListItem.vue';

export default {
  name: 'PharmacyList',
  components: {
    "pharmacy-list-item": PharmacyListItem
  },
  props: [
    'pharmacyData',
  ],
  data() {
    return {
      currentPharmacyId: 0,
    }
  },
  methods: {
    setPharmacyId(id) {
      this.currentPharmacyId = id;
      this.$emit('selectedPharmacy', id);
    }
  }
}
</script>

<style>

</style>
