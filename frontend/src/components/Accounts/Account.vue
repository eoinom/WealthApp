<template>
  <q-card               
    @click="
      updateSelectedAccountId(account.bankAccountId);
      var symbol = getCurrencySymbol(account.quotedCurrency.nameShort);
      updateSelectedAccountCurrencySymbol(symbol);
      updateTableColumn({ columnNo: 1, columnObj: { label: 'Value (' + account.quotedCurrency.code + ' ' + symbol + ')' } });
      "
    class="my-card text-white"
    style="background: radial-gradient(circle, #35a2ff 0%, #014a88 100%)" 
    >
    <q-card-section>
      <div class="row">
        
        <div class="col-9 text-h6">{{ account.accountName }}</div>

        <div class="col-3">
          <div class="float-right">
            <q-btn 
              @click.stop="showEditAccount = true"
              flat 
              round 
              dense
              color="cyan-2" 
              icon="edit" /> 
            <q-btn 
              @click.stop="promptToDeleteAccount(account.bankAccountId)"
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
      <br />Balance: {{ getCurrencySymbol(account.quotedCurrency.nameShort) + getBankAccountBalance(account.bankAccountId) }}
    </q-card-section>

    <q-tooltip anchor="top right" self="top middle" :offset="[10, 10]" 
      content-class="bg-deep-orange" content-style="font-size: 14px" >
      {{ account.description }}
    </q-tooltip>

    <q-dialog v-model="showEditAccount">
      <edit-account 
        @close="showEditAccount = false" 
        :account="account"
        :accountId="account.bankAccountId" />
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
        showEditAccount: false
      }      
    },
    methods: {
      ...mapActions('accounts', ['updateSelectedAccountId', 'updateSelectedAccountCurrencySymbol', 'updateTableColumn']),
      ...mapActions('main', ['deleteBankAccount']),
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
      ...mapGetters('main', ['bankAccounts', 'bankAccountValuesByAccountId', 'getBankAccountBalance']),
      ...mapGetters('accounts', ['selectedAccountId']),
    },

    components: {
      'edit-account': require('components/Accounts/Modals/EditAccount.vue').default
    }
  }
</script>

<style>

</style>