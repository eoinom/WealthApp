<template>
  <q-card               
    @click="
      updateSelectedAccountId(account.bankAccountId);
      updateTableColumn({ columnNo: 1, columnObj: { label: 'Value (' + account.quotedCurrency.code + ' ' + getCurrencySymbol(account.quotedCurrency.nameShort) + ')' } });
      "
    class="my-card text-white"
    style="background: radial-gradient(circle, #35a2ff 0%, #014a88 100%)" 
    >
    <q-card-section>
      <div class="row">
        
        <div class="col text-h6">{{ account.accountName }}</div>

        <div class="col">
          <div class="float-right">
            <q-btn 
              @click.stop="showEditAccount = true"
              flat 
              round 
              dense
              color="cyan-2" 
              icon="edit" /> 
            <q-btn 
              @click.stop="promptToDeleteAccount(id)"
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
      <br />Balance: {{ getCurrencySymbol(account.quotedCurrency.nameShort) + getAccountBalance(account.bankAccountId).toFixed(2) }}
    </q-card-section>

    <q-tooltip anchor="top right" self="top middle" :offset="[10, 10]" 
      content-class="bg-deep-orange" content-style="font-size: 14px" >
      {{ account.description }}
    </q-tooltip>

    <q-dialog v-model="showEditAccount">
      <edit-account 
        @close="showEditAccount = false" 
        :account="account"
        :id="id" />
    </q-dialog>
  </q-card>
</template>

<script>
  import { mapActions } from 'vuex'
  import { mapGetters } from 'vuex'

  export default {
    props: ['account', 'id'],
    data() {
      return {
        showEditAccount: false
      }      
    },
    methods: {
      ...mapActions('accounts', ['updateSelectedAccountId', 'updateTableColumn']),
      ...mapActions('main', ['deleteBankAccount']),
    //   ...mapActions('tasks', ['updateTask', 'deleteTask']),
    //   ...mapGetters('tasks', 'tasks'),
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
          this.deleteBankAccount(id)
        })
      },
      log: function(str) {
        console.log(str);
      },
      getAccountBalance: function (accountId) {
        try {
          var numOfValues = this.bankAccountValuesByAccountId(accountId).length;
          var accountValsSorted = this.bankAccountValuesByAccountId(accountId).slice().sort(function(a, b) {
              var dateA = new Date(a.date);
              var dateB = new Date(b.date);
              return dateA - dateB;
          });
          return accountValsSorted[ numOfValues - 1 ].value;
        }
        catch (error) {
          console.error(error); 
          return "";
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

    computed: {
      ...mapGetters('main', ['bankAccounts', 'bankAccountValuesByAccountId']),
      ...mapGetters('accounts', ['selectedAccountId']),
    },

    components: {
      'edit-account': require('components/Accounts/Modals/EditAccount.vue').default
    }
  }
</script>

<style>

</style>