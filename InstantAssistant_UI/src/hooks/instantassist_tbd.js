import { toRefs, reactive } from "vue";
import axios from "axios";
import useInstantAssistant from "../store";
const API_URL = "https://localhost:3000"; // "http://qa-agentassist-api.usa-ed.net"; // 
const state = reactive({
    error: null,
    showError: false,
    message: null,
    showMessage: false,
    loading: false,
    sideMenuOpen: false,
    clients: [],
    clientsInfo: {
        chatBotID: 0,
        chatBotName: "",
        iaConfigData: {
            description: "",
            active: true,
            urlSlug: "",
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
            defaultIntent: null,
            intents: []
        }
    },
    selectedIntent: -1,
    showIntentEditor: false,
    uid: "e12345",
});
export default () => {
    const instantAssistant = useInstantAssistant();
    const setShowError = (open) => {
        state.showError = open;
    };
    const setShowMessage = (open) => {
        state.showMessage = open;
    };
    const setSideMenuOpen = (open) => {
        state.sideMenuOpen = open;
    };
    const getClientList = async () => {
        state.loading = true;
        await instantAssistant.fetchClientList();
        axios.get(`${API_URL}/ClientsInfo`)
            .then(data => {
            state.clients = data.data;
        })
            .catch(error => {
            state.error = error.response.data;
            setShowError(true);
        })
            .finally(() => {
            state.loading = false;
        });
    };
    const getClientsInfo = async (id) => {
        state.loading = true;
        axios.get(`${API_URL}/ClientsInfo/${id}`)
            .then(data => {
            instantAssistant.clientsInfo = data.data;
            state.clientsInfo = data.data;
            setSideMenuOpen(true);
        })
            .catch(error => {
            state.error = error.response.data;
            setShowError(true);
        })
            .finally(() => {
            state.loading = false;
        });
    };
    const createClient = async (name) => {
        state.loading = true;
        axios.post(`${API_URL}/ClientsInfo`, {
            chatBotName: name
        })
            .then(data => {
            state.clients = [...state.clients, data.data];
            state.message = `Successfully created ${name}!`;
            setShowMessage(true);
        })
            .catch(error => {
            state.error = error.response.data;
            setShowError(true);
        })
            .finally(() => {
            state.loading = false;
        });
    };
    const saveClientsInfo = async () => {
        state.loading = true;
        axios.put(`${API_URL}/ClientsInfo/${state.clientsInfo.chatBotID}`, state.clientsInfo)
            .then(data => {
            state.message = data.data;
            setShowMessage(true);
        })
            .catch(error => {
            state.error = error.response.data;
            setShowError(true);
        })
            .finally(() => {
            state.loading = false;
        });
    };
    const deleteClient = async (id) => {
        state.loading = true;
        axios.delete(`${API_URL}/ClientsInfo/${id}`)
            .then(data => {
            state.message = data.data;
            setShowMessage(true);
            getClientList();
        })
            .catch(error => {
            state.error = error.response.data;
            setShowError(true);
        })
            .finally(() => {
            state.loading = false;
        });
    };
    const testIntents = async () => {
        state.loading = true;
        axios.post(`${API_URL}/AI/Intents/Test`, {
            ChatBotId: state.clientsInfo.chatBotID,
            intents: state.clientsInfo.iaConfigData.intents
        })
            .then(data => {
            state.message = `Successfully tested and saved intents!`;
            setShowMessage(true);
        })
            .catch(error => {
            state.error = error.response.data;
            setShowError(true);
        })
            .finally(() => {
            state.loading = false;
        });
    };
    const deployIntents = async () => {
        state.loading = true;
        axios.post(`${API_URL}/AI/Intents/Deploy`, {
            ChatBotId: state.clientsInfo.chatBotID,
            intents: state.clientsInfo.iaConfigData.intents
        })
            .then(data => {
            state.message = `Successfully deployed intents!`;
            setShowMessage(true);
        })
            .catch(error => {
            state.error = error.response.data;
            setShowError(true);
        })
            .finally(() => {
            state.loading = false;
        });
    };
    const setSelectedIntent = (index) => {
        state.showIntentEditor = false;
        state.selectedIntent = index;
        setTimeout(() => { state.showIntentEditor = true; }, 50);
    };
    return {
        ...toRefs(state),
        setShowError,
        setShowMessage,
        getClientList,
        getClientsInfo,
        createClient,
        saveClientsInfo,
        deleteClient,
        testIntents,
        deployIntents,
        setSelectedIntent
    };
};
//# sourceMappingURL=instantassist_tbd.js.map