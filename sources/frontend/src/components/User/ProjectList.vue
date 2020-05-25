<template>
  <el-table :data="items" class="project_list">
    <el-table-column
      prop="updateDate"
      label="Updated on"
      width="130px"
    ></el-table-column>

    <el-table-column prop="title" label="Title" width="150px"></el-table-column>

    <el-table-column
      prop="description"
      label="Description"
      width="400px"
    ></el-table-column>

    <el-table-column align="right">
      <template slot-scope="scope">
        <el-button
          size="mini"
          @click="handleEdit(scope.$index, scope.row)"
          icon="el-icon-edit"
        ></el-button>

        <el-popconfirm
          :title="$t('common.deleteWarning')"
          :confirmButtonText="$t('common.yes')"
          :cancelButtonText="$t('common.no')"
          @onConfirm="handleDelete(scope.$index, scope.row)"
          icon="el-icon-info"
          iconColor="red"
        >
          <el-button
            slot="reference"
            size="mini"
            type="danger"
            icon="el-icon-delete"
          ></el-button>
        </el-popconfirm>
      </template>
    </el-table-column>
  </el-table>
</template>

<script>
export default {
  data() {
    return {
      loading: false,
      items: [
        {
          id: '52e80f7a-2262-41d4-8eab-a730b1bcf741',
          title: 'Test site',
          description:
            'Voluptatibus illum vero eveniet explicabo deleniti repellat et maxime voluptates cum architecto. Laborum alias fuga voluptas expedita repudiandae nulla facere.',
          updateDate: '01.01.2020 16:45'
        },
        {
          id: '6344e30e-ef55-4b11-8e63-b2b5ebd4480e',
          title: 'Site about cats',
          description:
            'Quo, voluptatum, aliquid dolorum veritatis corrupti sunt impedit sed quisquam hic aliquam voluptatibus ab esse facere.',
          updateDate: '01.05.2020 09:03'
        },
        {
          id: 'f204b9d5-fe52-4154-adcb-47c07f45a10b',
          title: 'Math helper',
          description:
            ' Quibusdam esse labore laudantium odit est. Explicabo earum qui rerum officia, omnis eum quae amet culpa veritatis consequuntur ullam.',
          updateDate: '25.05.2020 22:12'
        }
      ]
    }
  }
}
</script>

<style lang="sass" scoped>
.project_list
  width: 100%
  overflow: auto
  max-height: 70vh
</style>
