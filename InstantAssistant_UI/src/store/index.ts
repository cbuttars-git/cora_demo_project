import { defineStore } from 'pinia';
import axios from "axios";

const API_URL = "https://localhost:22185/InstantAssistant"; //"https://localhost:5232/InstantAssistant";  "http://qa-agentassist-api.usa-ed.net/InstantAssistant"; // "http://172.18.80.131:8080/InstantAssistant"; // 

interface UserExample {
  id: number;
  text: string;
  active: boolean;
  updated: boolean;
  aIModelTestDate: string;
  aIModelDeployDate: string;
}

interface Intent {
  id: number;
  name: string;
  response: string;
  url: string,
  active: boolean;
  updated: boolean;
  aIModelTestDate: string;
  aIModelDeployDate: string;
  userExamples: UserExample[];
}

interface DisplayConfig {
  headerBgColor: string;
  headerFgColor: string;
  footerBgColor: string;
  footerFgColor: string;
  dialogBgColor: string;
  dialogFgColor: string;
  linkColor: string;
  logo: string;
}

interface IAConfigData {
  description: string;
  active: boolean;
  userAdGroup: string;
  adminAdGroup: string;
  placeholder: string;
  displayConfig: DisplayConfig;
  welcomeIntent: Intent | null;
  errorIntent: Intent | null;
  intents: Intent[];
}

interface IClientsInfo {
  chatBotID: number;
  chatBotName: string;
  iaConfigData: IAConfigData;
}

class ClientsInfo implements IClientsInfo {
  chatBotID = -1;
  chatBotName = "";
  iaConfigData: IAConfigData = {
    description: "",
    active: true,
    userAdGroup: "",
    adminAdGroup: "",
    placeholder: "",
    displayConfig: {
      headerBgColor: "",
      headerFgColor: "",
      footerBgColor: "",
      footerFgColor: "",
      dialogBgColor: "",
      dialogFgColor: "",
      linkColor: "",
      logo: "",
    },
    welcomeIntent: null,
    errorIntent: null,
    intents: []
  };
}

class Client {
  id = -1;
  name = "";
  logo = "";
}

interface IntentTestResults {
  new_Intent: string[],
  new_Intents_ID: number[],
  new_Userexample: string[],
  new_Userexample_ID: number[]
  old_Intent: string[],
  old_Intent_ID: number[],
  old_Userexample: string[],
  old_Userexample_ID: number[],
  similarity_Score: number[]
}

interface IntentTestResponse {
  message: string,
  results: IntentTestResults
}

const useInstantAssistant = defineStore('clientsInfo', {
  state: () => {
    return {
      error: "",
      showError: false,
      message: "",
      showMessage: false,
      intentTestResponse: null as IntentTestResponse | null,
      showIntentTestResponse: false,
      isLoading: false,
      showSideMenu: false,
      showIntentEditor: false,
      isAdmin: false,
      clients: [] as Client[],
      clientsInfo: new ClientsInfo(),
      selectedIntentIndex: -1,
      uid: ""
    }
  },
  getters: {
    nextIntentIndex(): number {
      return this.clientsInfo.iaConfigData.intents.length;
    },
    selectedIntent(): Intent {
      if (this.selectedIntentIndex == -2) {
        if(this.clientsInfo.iaConfigData.welcomeIntent == null) {
          this.clientsInfo.iaConfigData.welcomeIntent = {
            id: -1,
            name: "Welcome Intent",
            response: "",
            url: "",
            active: true,
            updated: false,
            aIModelTestDate: "",
            aIModelDeployDate: "",
            userExamples: []
          }
        }
        return this.clientsInfo.iaConfigData.welcomeIntent;
      } else if(this.selectedIntentIndex == -1) {
        if(this.clientsInfo.iaConfigData.errorIntent == null) {
          this.clientsInfo.iaConfigData.errorIntent = {
            id: -1,
            name: "Error Intent",
            response: "",
            url: "",
            active: true,
            updated: false,
            aIModelTestDate: "",
            aIModelDeployDate: "",
            userExamples: []
          }
        }
        return this.clientsInfo.iaConfigData.errorIntent;
      } else {
        return this.clientsInfo.iaConfigData.intents[this.selectedIntentIndex];
      }
    },
    getLogo(): string {
      return this.clientsInfo.iaConfigData.displayConfig.logo;
    },
    clientsInfoHasChanged(): boolean {
      const prevClientsInfo = sessionStorage.getItem("clientsInfo");
      if(prevClientsInfo) {
        const prev = JSON.parse(prevClientsInfo);
        if(prev.chatBotID > -1) {
          return prevClientsInfo !== JSON.stringify(this.clientsInfo);
        }
      }

      return false;
    }
  },
  actions: {
    async fetchIsAdmin() {
      axios.get(`${API_URL}/Admin/IsAdmin`, { withCredentials: true })
      .then(res => {
        this.isAdmin = res.data;
      })
      .catch(error => {
        this.error = error.response.data;
      })
      .finally();
    },
    async fetchClientList() {
      this.isLoading = true;
      axios.get(`${API_URL}/ClientsInfo`, { withCredentials: true })
      .then(res => {
        this.clients = res.data;
      })
      .catch(error => {
        this.error = error.response.data;
        this.showError = true;
      })
      .finally(() => {
        this.isLoading = false;
      });
    },
    async fetchClientsInfo(id: number) {
      this.isLoading = true;
      axios.get(`${API_URL}/ClientsInfo/${id}`, { withCredentials: true })
        .then(data => {
          this.clientsInfo = data.data;
          sessionStorage.setItem("clientsInfo", JSON.stringify(this.clientsInfo));
          this.showSideMenu = true;
        })
        .catch(error => {
          this.error = error.response.data;
          this.showError = true;
        })
        .finally(() => {
          this.isLoading = false;
        });
    },
    setLogo(img: string) {
      this.clientsInfo.iaConfigData.displayConfig.logo = img;
    },
    async createClient(name: string) {
      this.isLoading = true;
      axios.post(`${API_URL}/ClientsInfo`, {
        chatBotName: name
      }, { withCredentials: true })
        .then(data => {
          this.clients = [...this.clients, data.data];
          this.message = `Successfully created ${name}!`;
          this.showMessage = true;
        })
        .catch(error => {
          this.error = error.response.data;
          this.showError = true;
        })
        .finally(() => {
          this.isLoading = false;
        });
    },
    async saveClientsInfo() {
      this.isLoading = true;
      
      axios.put(`${API_URL}/ClientsInfo/${this.clientsInfo.chatBotID}`, this.clientsInfo, { withCredentials: true })
        .then(data => {
          sessionStorage.setItem("clientsInfo", JSON.stringify(this.clientsInfo));
          this.message = data.data;
          this.showMessage = true;
        })
        .catch(error => {
          this.error = error.response.data;
          this.showError = true;
        })
        .finally(() => {
          this.isLoading = false;
        });
    },
    async deleteClient(id: number) {
      this.isLoading = true;
      axios.delete(`${API_URL}/ClientsInfo/${id}`, { withCredentials: true })
        .then(data => {
          this.message = data.data;
          this.showMessage = true;
          this.fetchClientList();
        })
        .catch(error => {
          this.error = error.response.data;
          this.showError = true;
        })
        .finally(() => {
          this.isLoading = false;
        });
    },
    resetIntentIds() {
      const intents = this.clientsInfo.iaConfigData.intents.map((intent, i) => {
        intent.id = i + 1;

        const userExamples = intent.userExamples.map((example, ui) => {
          example.id = ui + 1;
          return example;
        });

        intent.userExamples = userExamples;

        return intent;
      });
      this.clientsInfo.iaConfigData.intents = intents;
    },
    createIntent(name: string) {
      const intent = this.clientsInfo.iaConfigData.intents.find(intent => intent.name.toLowerCase() === name.toLowerCase());
      if(intent) {
        this.error = `An intent with a name of "${name}" already exists!`;
        this.showError = true;
      } else {
        this.clientsInfo.iaConfigData.intents.push({
          id: this.nextIntentIndex,
          name: name,
          response: "",
          url: "",
          active: true,
          updated: true,
          aIModelTestDate: "",
          aIModelDeployDate: "",
          userExamples: [],
        });

        this.resetIntentIds();
      }
    },
    deleteIntent(index: number) {
      this.clientsInfo.iaConfigData.intents.splice(index, 1);
      this.resetIntentIds();
    },
    async testIntents() {
      this.isLoading = true;

      this.resetIntentIds();

      axios.post(`${API_URL}/AI/Intents/Test`, {
        ChatBotId: this.clientsInfo.chatBotID,
        intents: this.clientsInfo.iaConfigData.intents
      }, { withCredentials: true })
        .then(async res => {
          this.intentTestResponse = res.data;
          if(res.data.results === null) {
            const now = new Intl.DateTimeFormat('en-US', {
              year: 'numeric', month: 'numeric', day: 'numeric',
              hour: 'numeric', minute: 'numeric', second: 'numeric',
              hour12: false,
              timeZone: 'America/New_York'
            }).format(Date.now());
    
            this.clientsInfo.iaConfigData.intents.forEach((intent, ii) => {
              if(intent.updated) {
                intent.updated = false;
                intent.aIModelTestDate = now
    
                intent.userExamples.forEach((example, ei) => {
                  if(example.updated) {
                    example.updated = false;
                    example.aIModelTestDate = now;
    
                    intent.userExamples[ei] = example;
                  }
                });
    
                this.clientsInfo.iaConfigData.intents[ii] = intent;
              }
            });

            await this.saveClientsInfo();
            
            this.message = `Successfully tested and saved intents!`;
            this.showMessage = true;
          } else {
            this.showIntentTestResponse = true;
          }
        })
        .catch(error => {
          this.error = error.response.data;
          this.showError = true;
        })
        .finally(() => {
          this.isLoading = false;
        });
    }
  }
});

export default useInstantAssistant;