<template>
  <div>
    <h3 class="tab-title">{{ $t('tabs.publish.name') }}</h3>

    <DeployType
      deploy-type="ftp"
      icon="ftp-icon el-icon-folder-checked"
      @deploy="ftpDeploy"
    >
      <FtpModal />
    </DeployType>

    <el-divider></el-divider>

<!--    <DeployType deploy-type="github" icon="github" @deploy="githubDeploy">-->
<!--      <GithubModal />-->
<!--    </DeployType>-->

<!--    <el-divider></el-divider>-->

    <DeployType deploy-type="vercel" icon="vercel" @deploy="vercelDeploy">
      <VercelModal />
    </DeployType>
  </div>
</template>

<script>
import DeployType from './DeployType.vue'
import FtpModal from './FtpModal.vue'
import GithubModal from './GithubModal.vue'
import VercelModal from './VercelModal.vue'

import { FTP_MODAL, GITHUB_MODAL, VERCEL_MODAL } from '@/store/modals'

export default {
  data: () => ({
    FTP_MODAL,
    GITHUB_MODAL,
    VERCEL_MODAL
  }),

  components: {
    DeployType,
    FtpModal,
    // GithubModal,
    VercelModal
  },

  methods: {
    ftpDeploy() {
      this.$modal.open(FTP_MODAL)
    },
    githubDeploy() {
      this.$modal.open(GITHUB_MODAL)
    },
    vercelDeploy() {
      this.$modal.open(VERCEL_MODAL)
    }
  }
}
</script>
