<template>
  <div class="container-fluid mt-4">
    <h1 class="h1">Medicine Details</h1>
    <b-row>
      <b-col lg="6">
        <b-card :title="(model.id ? 'Edit : ' + model.name : 'New Medicine Record')">
          <form @submit.prevent="createMedicineRecord">
            <b-form-group label="Name">
              <b-form-input type="text" v-model="model.name"></b-form-input>
            </b-form-group>
            <b-form-group label="Brand">
              <b-form-input type="text" v-model="model.brand"></b-form-input>
            </b-form-group>
            <b-form-group label="Price">
              <b-form-input rows="4" v-model="model.price" type="number"></b-form-input>
            </b-form-group>
             <b-form-group label="Quanity">
              <b-form-input rows="4" v-model="model.quantity" type="number"></b-form-input>
            </b-form-group>
            <b-form-group label="Expiry Date">
              <b-form-input rows="4" v-model="model.expiryDate" type="date"></b-form-input>
            </b-form-group>
            <b-form-group label="Notes">
              <b-form-input rows="4" v-model="model.notes" type="text"></b-form-input>
            </b-form-group>
            <div>
              <b-btn type="submit" variant="success">Save Record</b-btn>
            </div>
          </form>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script>
  import api from '@/MedicineAPIService';

  export default {
     
    data() {
      return {
        name: "",
        model: {}
      };
    },
    async created() {
      this.getAll()
    },
    methods: {
      async getAll() {
        this.loading = true

        try {
          this.records = await api.getAll()
        } finally {
          this.loading = false
        }
      },
      async updateMedicineRecord(medicineRecord) {
        // We use Object.assign() to create a new (separate) instance
        this.model = Object.assign({}, medicineRecord)
      },
      async createMedicineRecord() {
        const isUpdate = !!this.model.id;
        if(this.model.name != null && this.model.quantity != null && this.model.price != null && this.model.expiryDate != null)
        {
            try{
        if (isUpdate) {
          await api.update(this.model.id, this.model)
        } else {
          await api.create(this.model)
        }
        }
        catch(err)
        {
            alert("Something went wrong!");
        }
        // Clear the data inside of the form
        this.model = {}

        // Fetch all records again to have latest data
        await this.getAll()
        }
        else{
            alert("Please fill all details first");
        }

        // Clear the data inside of the form
        this.model = {}

        // Fetch all records again to have latest data
        await this.getAll()
      },
      async deleteMedicineRecord(id) {
        if (confirm('Are you sure you want to delete this record?')) {
          // if we are editing a food record we deleted, remove it from the form
          if (this.model.id === id) {
            this.model = {}
          }

          await api.delete(id)
          await this.getAll()
        }
      },

    },
    computed: {
    filteredList() {
      return this.records.filter(record => {
        return record.name.toLowerCase().includes(this.name.toLowerCase())
      })
    }
  }
}
</script>