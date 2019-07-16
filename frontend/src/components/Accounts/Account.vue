<template>
  <q-card               
    @click="updateForSelectedId()"
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
      <div class="row"> 
        <div class="col-9">
          Type: {{ account.type }}
          <br />Currency: {{ account.quotedCurrency.code }}
          <br />Balance: {{ getCurrencySymbol(account.quotedCurrency.nameShort) + getAccountBalance(account.accountId) }}
        </div>
        <div class="col institutionLogo" v-if="getInstitutionUrl(account.institution) != ''">
          <img :src="getInstitutionLogoSrc(account.institution, undefined)">
        </div>
      </div>
    </q-card-section>

    <q-tooltip anchor="top right" self="top middle" :offset="[10, 10]" 
      content-class="bg-deep-orange" content-style="font-size: 14px" >
      {{ account.description }}
    </q-tooltip>

    <q-dialog v-model="showEditAccount">
      <edit-account 
        @close="showEditAccount = false" 
        :account="account"
        :accountId="account.accountId" />
    </q-dialog>
  </q-card>
</template>

<script>
  import { mapActions } from 'vuex'
  import { mapGetters } from 'vuex'
  import { institutionUrlsMixin } from '../../mixins/institutionUrlsMixin'

  export default {
    props: ['account'],
    mixins: [institutionUrlsMixin],

    data() {
      return {
        showEditAccount: false
      }      
    },

    computed: {
      ...mapGetters('accounts', ['accounts', 'accountValuesByAccountId', 'accountBalance', 'selectedAccountId']),
      ...mapGetters('main', ['userDisplayCurrencyCode'])
    },

    methods: {
      ...mapActions('accounts', ['updateSelectedAccountId', 'updateSelectedAccountCurrencySymbol', 'updateTableColumn', 'deleteAccount', 
      'addToVisibleColumns', 'removeFromVisibleColumns']),      

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
            // return this.accountBalance(accountId).toFixed(2);
            return this.toLocaleFixed(this.accountBalance(accountId), 2);
          }          
        }
        catch (error) {
          console.error(error); 
          return " Not available";
        }        
      },
      updateForSelectedId: function () {
        this.updateSelectedAccountId(this.account.accountId);
        var symbol = this.getCurrencySymbol(this.account.quotedCurrency.nameShort);
        this.updateSelectedAccountCurrencySymbol(symbol);
        this.updateTableColumn({ columnNo: 1, columnObj: { label: 'Value (' + this.account.quotedCurrency.code + ' ' + symbol + ')' } });
        
        if (this.userDisplayCurrencyCode !== this.account.quotedCurrency.code) {
          symbol = this.getCurrencySymbol(this.userDisplayCurrencyCode);
          this.updateTableColumn({ columnNo: 3, columnObj: { label: 'Value (' + this.userDisplayCurrencyCode + ' ' + symbol + ')' } })
          this.addToVisibleColumns('rateToUserCurrency')
          this.addToVisibleColumns('valueUserCurrency')
        }
        else {
          this.removeFromVisibleColumns('rateToUserCurrency')
          this.removeFromVisibleColumns('valueUserCurrency')
        }        
      }
    },

    components: {
      'edit-account': require('components/Accounts/Modals/EditAccount.vue').default
    }
  }
</script>

<style>
  .institutionLogo {
    text-align: right;
  }

  .institutionLogo img {
    max-width: 150px;
    max-height: 70px;
    height: auto;
  }
</style>