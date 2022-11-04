import { toRefs, reactive } from "vue";
import axios from "axios";

import useInstantAssistant from "../store";

const API_URL = "https://localhost:22185"; // "http://qa-agentassist-api.usa-ed.net"; // 

interface UserExample {
  id: number;
  text: string;
  active: boolean;
  updated: boolean;
  modifiedDate: number;
  aIModelTestDate: number;
  aIModelDeployDate: number;
}

interface Intent {
  id: number;
  name: string;
  response: string;
  url: string,
  active: boolean;
  updated: boolean;
  modifiedDate: number;
  aIModelTestDate: number;
  aIModelDeployDate: number;
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
  urlSlug: string;
  active: boolean;
  userAdGroup: string;
  adminAdGroup: string;
  placeholder: string;
  displayConfig: DisplayConfig;
  defaultIntent: Intent | null;
  intents: Intent[];
}

interface ClientsInfo {
  chatBotID: number;
  chatBotName: string;
  iaConfigData: IAConfigData;
}

interface Client {
  id: number;
  name: string;
}

const state = reactive<{
  error: any;
  showError: boolean,
  message: any;
  showMessage: boolean,
  loading: boolean;
  sideMenuOpen: boolean;
  clients: Client[];
  clientsInfo: ClientsInfo;
  selectedIntent: number;
  showIntentEditor: boolean;
  uid: string;
}>({
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

  const setShowError = (open: boolean) => {
    state.showError = open;
  }
  
  const setShowMessage = (open: boolean) => {
    state.showMessage = open;
  }
  
  const setSideMenuOpen = (open: boolean) => {
    state.sideMenuOpen = open;
  }

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
  }

  const getClientsInfo = async (id: number) => {
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
  }

  const createClient = async (name: string) => {
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
  }

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
  }

  const deleteClient = async (id: number) => {
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
  }

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
  }

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
  }

  const setSelectedIntent = (index: number) => {
    state.showIntentEditor = false;
    state.selectedIntent = index;
    setTimeout(() => {state.showIntentEditor = true;}, 50);
  }

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