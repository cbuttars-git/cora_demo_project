import { defineStore } from 'pinia';
import axios from "axios";
import { useMsal } from "@/msal/useMsal";
import { loginRequest, graphConfig } from "@/authConfig";
const API_URL = "https://localhost:5232/InstantAssistant"; // "http://qa-agentassist-api.usa-ed.net/InstantAssistant"; // "http://172.18.80.131:8080/InstantAssistant"; // 
class ClientsInfo {
    chatBotID = -1;
    chatBotName = "";
    iaConfigData = {
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
    adminAdGroup = "";
}
const useInstantAssistant = defineStore('instantAssistant', {
    state: () => {
        return {
            error: "",
            showError: false,
            message: "",
            showMessage: false,
            intentTestResponse: null,
            showIntentTestResponse: false,
            isLoading: false,
            showSideMenu: false,
            showIntentEditor: false,
            clients: [],
            clientsInfo: new ClientsInfo(),
            selectedIntentIndex: -1,
            uid: "",
            adGroups: []
        };
    },
    getters: {
        async accessToken() {
            const { instance } = useMsal();
            const res = await instance.acquireTokenSilent({ ...loginRequest });
            return res.accessToken;
        },
        isAdmin() {
            return this.adGroups.findIndex((group) => {
                return (group.displayName === "GGR_A_InstantAssistant_D_UserModify");
            }) > -1;
        },
        nextIntentIndex() {
            return this.clientsInfo.iaConfigData.intents.length;
        },
        selectedIntent() {
            if (this.selectedIntentIndex == -2) {
                if (this.clientsInfo.iaConfigData.welcomeIntent == null) {
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
                    };
                }
                return this.clientsInfo.iaConfigData.welcomeIntent;
            }
            else if (this.selectedIntentIndex == -1) {
                if (this.clientsInfo.iaConfigData.errorIntent == null) {
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
                    };
                }
                return this.clientsInfo.iaConfigData.errorIntent;
            }
            else {
                return this.clientsInfo.iaConfigData.intents[this.selectedIntentIndex];
            }
        },
        getLogo() {
            return this.clientsInfo.iaConfigData.displayConfig.logo;
        },
        clientsInfoHasChanged() {
            const prevClientsInfo = sessionStorage.getItem("clientsInfo");
            if (prevClientsInfo) {
                const prev = JSON.parse(prevClientsInfo);
                if (prev.chatBotID > -1) {
                    return prevClientsInfo !== JSON.stringify(this.clientsInfo);
                }
            }
            return false;
        }
    },
    actions: {
        inGroup(groupName) {
            return this.adGroups.findIndex((group) => {
                return (group.displayName === groupName);
            }) > -1;
        },
        async fetchADGroups() {
            const accessToken = await this.accessToken;
            axios.get(`${graphConfig.graphMeEndpoint}/memberOf/microsoft.graph.group?$count=true&$select=id,displayName`, {
                headers: {
                    Authorization: `Bearer ${accessToken}`,
                    ConsistencyLevel: "eventual"
                }
            })
                .then(res => {
                this.adGroups = res.data.value;
            })
                .catch(error => {
                this.error = error.response.data;
                this.showError = true;
            })
                .finally();
        },
        async fetchClientList() {
            this.isLoading = true;
            await this.fetchADGroups();
            axios.get(`${API_URL}/ClientsInfo`, { withCredentials: true })
                .then(res => {
                this.clients = res.data.filter((client) => {
                    return this.isAdmin || this.inGroup(client.adminAdGroup);
                });
            })
                .catch(error => {
                this.error = error.response.data;
                this.showError = true;
            })
                .finally(() => {
                this.isLoading = false;
            });
        },
        async fetchClientsInfo(id) {
            this.isLoading = true;
            await this.fetchADGroups();
            axios.get(`${API_URL}/ClientsInfo/${id}`, { withCredentials: true })
                .then(res => {
                setTimeout(() => {
                    if (this.isAdmin || this.inGroup(this.clientsInfo.iaConfigData.adminAdGroup)) {
                        this.clientsInfo = res.data;
                        sessionStorage.setItem("clientsInfo", JSON.stringify(this.clientsInfo));
                        this.showSideMenu = true;
                    }
                }, 500);
            })
                .catch(error => {
                this.error = error.response.data;
                this.showError = true;
            })
                .finally(() => {
                this.isLoading = false;
            });
        },
        setLogo(img) {
            this.clientsInfo.iaConfigData.displayConfig.logo = img;
        },
        async createClient(name) {
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
        async deleteClient(id) {
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
        createIntent(name) {
            const intent = this.clientsInfo.iaConfigData.intents.find(intent => intent.name.toLowerCase() === name.toLowerCase());
            if (intent) {
                this.error = `An intent with a name of "${name}" already exists!`;
                this.showError = true;
            }
            else {
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
        deleteIntent(index) {
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
                .then(async (res) => {
                this.intentTestResponse = res.data;
                if (res.data.results === null) {
                    const now = new Intl.DateTimeFormat('en-US', {
                        year: 'numeric', month: 'numeric', day: 'numeric',
                        hour: 'numeric', minute: 'numeric', second: 'numeric',
                        hour12: false,
                        timeZone: 'America/New_York'
                    }).format(Date.now());
                    this.clientsInfo.iaConfigData.intents.forEach((intent, ii) => {
                        if (intent.updated) {
                            intent.updated = false;
                            intent.aIModelTestDate = now;
                            intent.userExamples.forEach((example, ei) => {
                                if (example.updated) {
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
                }
                else {
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
//# sourceMappingURL=index.js.map