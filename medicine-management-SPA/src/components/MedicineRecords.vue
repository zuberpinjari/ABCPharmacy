<template>
  <div class="container-fluid mt-4">
    <h1 class="h1">Medicine Records</h1>
    <b-alert :show="loading" variant="info">Loading...</b-alert>
    <b-row>
    <b-col>
    <div class="col-md-8">
      <div class="input-group mb-3">
        <input type="text" class="form-control" placeholder="Search by Name"
          v-model="name"/>
      </div>
    </div>
    </b-col>
    <b-col lg=3>
      <div>
        <b-btn type="submit" v-on:click="AddRecord()" variant="success">Add Medicine</b-btn>
      </div>
    </b-col>
    </b-row>
    <b-row>
      <b-col>
        <table  id="dtBasic" class="table">
          <thead>
            <tr>
              <th>Name</th>
              <th>Brand</th>
              <th>Price</th>
              <th>Quantity</th>
              <th type="date">Expiry Date</th>
              <th>&nbsp;</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="record in filteredList" :key="record.id" >
              <td >{{ record.name }}</td>
              <td>{{ record.brand }}</td>
              <td>{{ record.price }}</td>
              <td :style="{background : record.quantity < 10 ? 'yellow' : 'white' }">{{ record.quantity }}</td>
              <td type= "date" :style="{background : Date.parse(record.expiryDate) - (30 * 1000 * 60 * 60 * 24) < Date.now() ? 'red' : 'white' }">{{ record.expiryDate }}</td>
              <td class="text-right">
                <a href="#" @click.prevent="updateMedicineRecord(record)">Edit</a> -
                <a href="#" @click.prevent="deleteMedicineRecord(record.id)">Delete</a>
              </td>
            </tr>
          </tbody>
        </table>
      </b-col>
      <b-col lg="3">
        <b-card :title="(model.id ? 'Edit : ' + model.name : 'Click Edit to update records')">
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
      props : {
          record : {}
      },
    data() {
      return {
        loading: false,
        records: [],
        model: {},
        name: ""
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

      AddRecord: function () {
        // console.log("clickList fired with " + record.name);
         this.$router.push({ path: '/medicine-details/'})
        },

      async updateMedicineRecord(foodRecord) {
        // We use Object.assign() to create a new (separate) instance
        this.model = Object.assign({}, foodRecord)
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