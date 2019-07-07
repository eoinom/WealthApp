<template>
  <q-card               
    @click="
      updateSelectedAccountId(account.accountId);
      var symbol = getCurrencySymbol(account.quotedCurrency.nameShort);
      updateSelectedAccountCurrencySymbol(symbol);
      updateTableColumn({ columnNo: 1, columnObj: { label: 'Value (' + account.quotedCurrency.code + ' ' + symbol + ')' } });
      "
    class="my-card text-white"
    style="background: radial-gradient(circle, #BB5601 0%, #883F01 100%)" 
    >
    <q-card-section>
      <div class="row">
        
        <div class="col-9 text-h6">{{ account.accountName }}</div>

        <div class="col-3">
          <div class="float-right">
            <q-btn 
              @click.stop="showEditLiability = true"
              flat 
              round 
              dense
              color="cyan-2" 
              icon="edit" /> 
            <q-btn 
              @click.stop="promptToDeleteAccount(account.accountId)"
              flat 
              round 
              dense
              color="red-5" 
              icon="delete" />  
          </div>        
        </div>
      </div>

      <div class="text-subtitle2">{{ account.institution }}</div>
    </q-card-section>
    <q-card-section>
      Type: {{ account.type }}
      <br />Currency: {{ account.quotedCurrency.code }}
      <br />Balance: {{ getCurrencySymbol(account.quotedCurrency.nameShort) + getAccountBalance(account.accountId) }}
    </q-card-section>

    <q-tooltip anchor="top right" self="top middle" :offset="[10, 10]" 
      content-class="bg-deep-orange" content-style="font-size: 14px" >
      {{ account.description }}
    </q-tooltip>

    <q-dialog v-model="showEditLiability">
      <edit-liability 
        @close="showEditLiability = false" 
        :account="account"
        :accountId="account.accountId" />
    </q-dialog>
  </q-card>
</template>

<script>
  import { mapActions } from 'vuex'
  import { mapGetters } from 'vuex'

  export default {
    props: ['account'],
    data() {
      return {
        showEditLiability: false
      }      
    },

    computed: {
      ...mapGetters('liabilities', ['accounts', 'accountValuesByAccountId', 'getAccountBalance', 'selectedAccountId']),
    },
    
    methods: {
      ...mapActions('liabilities', ['updateSelectedAccountId', 'updateSelectedAccountCurrencySymbol', 'updateTableColumn', 'deleteAccount']),

      promptToDeleteAccount(id) {
        this.$q.dialog({
          title: 'Confirm',
          message: 'Are you sure you want to delete this account and all associated data? This cannot be undone.',
          ok: {
            label: 'Yes'
          },
          cancel: {
            color: 'negative'
          },
          persistent: true
        }).onOk(() => {
          this.deleteAccount(id)
        })
      },
      log: function(str) {
        console.log(str);
      },
      getAccountBalance: function (accountId) {
        try {
          var numOfValues = this.accountValuesByAccountId(accountId).length;
          if (numOfValues == 0) {
            console.log('No account values for accountId: ' + accountId);
            return " No account values";
          }
          else {
            return this.getAccountBalance(accountId).toFixed(2);
          }          
        }
        catch (error) {
          console.error(error); 
          return " Not available";
        }        
      },
      getCurrencySymbol: function (shortName) {
        try {
          switch ( shortName.toLowerCase() ) {
            case 'euro':
              return '€';
              break;
            case 'dollar':
            case 'peso':
              return '$';
              break;
            case 'pound':
              return '£';
              break;
            default:
              return '';
          }
        }
        catch (error) {
          console.error(error); 
          return "";
        } 
      },
    },

    components: {
      'edit-liability': require('components/Liabilities/Modals/EditLiability.vue').default
    }
  }
</script>

<style>

</style>